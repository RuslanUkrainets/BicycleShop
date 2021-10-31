
let bicyclesList = document.getElementById('bicyclesList')
let token

document.getElementById('authorizeBtn').addEventListener('click', function (e) {
    getTokenAsync()
})

async function getTokenAsync() {
    const credentials = {
        login: document.getElementById('email').value,
        password: document.getElementById('password').value
    }

    const response = await fetch(`api/account/token`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(credentials)
    })

    const data = await response.json()

    if (response.ok === true) {
        sessionStorage.setItem('access_token', data.access_token)
        showTime()
    } else {
        console.log(response.status, data.error)
    }
}

function showTime() {
    token = sessionStorage.getItem('access_token')

    if (token == null) return

    bicyclesList.innerHTML = getTable()

    let rightCol = document.getElementById('rightCol')
    rightCol.innerHTML = ''
    rightCol.append(createAddBtn())
    rightCol.append(createListBtn())

    getBicycles()
}

async function getBicycles() {
    const response = await fetch(`api/bicycles`, {
        headers: {
            'Authorization': 'bearer ' + token
        }
    })

    if (response.ok === true) {
        const bicycles = await response.json()
        let rows = document.querySelector('tbody')
        bicycles.forEach(bicycle => rows.append(createTableRow(bicycle)))
    }
}

async function createBicycle(name, wheelDiameter, color, chain, weight, year, price) {
    const response = await fetch('api/bicycles', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + token
        },
        body: JSON.stringify({
            name,
            wheelDiameter,
            color,
            chain,
            weight,
            year,
            price: +price
        })
    })

    if (response.ok === true) {
        showTime()
    }
}

function createBicycleEvent(e) {
    e.preventDefault()
    const form = document.forms['addBicycleForm']
    const name = form.elements['name'].value
    const wheelDiameter = form.elements['wheelDiameter'].value
    const color = form.elements['color'].value
    const chain = form.elements['chain'].value
    const weight = form.elements['weight'].value
    const year = form.elements['year'].value
    const price = form.elements['price'].value

    createBicycle(name, wheelDiameter, color, chain, weight, year, price)
}


showTime()


function createTableRow(bicycle) {
    const tr = document.createElement('tr')

    const titleTd = document.createElement('td')
    titleTd.append(bicycle.name)
    tr.append(titleTd)

    const diameterTd = document.createElement('td')
    diameterTd.append(bicycle.wheelDiameter)
    tr.append(diameterTd)

    const colorTd = document.createElement('td')
    colorTd.append(bicycle.color)
    tr.append(colorTd)

    const priceTd = document.createElement('td')
    priceTd.append(bicycle.price)
    tr.append(priceTd)

    const updateTr = document.createElement('td')
    updateTr.append(createUpdateBtn(bicycle))
    tr.append(updateTr)

    const deleteTr = document.createElement('td')
    deleteTr.append(createDeleteBtn(bicycle))
    tr.append(deleteTr)

    return tr
}

function createUpdateBtn(bicycle) {
    const updateBtn = document.createElement('button')
    updateBtn.value = bicycle.id
    updateBtn.innerText = 'Update'
    updateBtn.classList.add('btn', 'btn-success')
    updateBtn.addEventListener('click', updateBtnEvent)
    return updateBtn
}

function createDeleteBtn(bicycle) {
    const deleteBtn = document.createElement('button')
    deleteBtn.value = bicycle.id
    deleteBtn.innerText = 'Delete'
    deleteBtn.classList.add('btn', 'btn-danger')
    deleteBtn.addEventListener('click', deleteBtnEvent)
    return deleteBtn
}

function createAddBtn() {
    const addBtn = document.createElement('button')
    addBtn.innerText = 'Add Bicycle'
    addBtn.classList.add('btn', 'btn-primary', 'my-3', 'mx-3')
    addBtn.addEventListener('click', function () {
        bicyclesList.innerHTML = getAddForm()
        document.forms['addBicycleForm'].addEventListener('submit', createBicycleEvent)
    })
    return addBtn
}

function createListBtn() {
    const listBtn = document.createElement('button')
    listBtn.innerText = 'List bicycles'
    listBtn.classList.add('btn', 'btn-primary', 'my-3', 'mx-3')
    listBtn.addEventListener('click', function () {
        showTime()
    })
    return listBtn
}

function updateBtnEvent(e) {
    let id = this.value

    updateByID(id)    
}

async function updateByID(id) {

    let response = await fetch(`api/bicycles/${id}`, {
        method: 'GET',
        headers: {
            'Authorization': 'bearer ' + token
        }
    })

    if (response.ok === true) {
        let data = await response.json()

        bicyclesList.innerHTML = getAddForm()

        const form = document.forms['addBicycleForm']
        form.elements['bicycleId'].value = data['id']
        form.elements['name'].value = data['name']
        form.elements['wheelDiameter'].value = data['wheelDiameter']
        form.elements['color'].value = data['color']
        form.elements['chain'].value = data['chain']
        form.elements['weight'].value = data['weight']
        form.elements['year'].value = data['year']
        form.elements['price'].value = data['price']

        document.forms['addBicycleForm'].addEventListener('submit', updateBicycleEvent)
    }    
}

function updateBicycleEvent(e) {
    e.preventDefault()
    const form = document.forms['addBicycleForm']
    const id = form.elements['bicycleId'].value
    const name = form.elements['name'].value
    const wheelDiameter = form.elements['wheelDiameter'].value
    const color = form.elements['color'].value
    const chain = form.elements['chain'].value
    const weight = form.elements['weight'].value
    const year = form.elements['year'].value
    const price = form.elements['price'].value

    updateBicycle(id, name, wheelDiameter, color, chain, weight, year, price)
}

async function updateBicycle(id, name, wheelDiameter, color, chain, weight, year, price) {
    const response = await fetch('api/bicycles', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + token
        },
        body: JSON.stringify({
            id,
            name,
            wheelDiameter,
            color,
            chain,
            weight,
            year,
            price: +price
        })
    })

    if (response.ok === true) {
        showTime()
    }
}

function deleteBtnEvent(e) {
    let id = this.value
    deleteById(id)
}

async function deleteById(id){
    let response = await fetch(`api/bicycles/${id}`, {
        method: 'DELETE',
        headers: {
            'Authorization': 'bearer ' + token
        }
    })

    if (response.ok === true) {
        showTime()
    }
}

function getTable() {
    return `<table class="table">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>WheelDiameter</th>
                            <th>Color</th>
                            <th>Price</th>
                            <th>Update</th>
                            <th>Delete</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>`
}

function getAddForm() {
    return `<form id="addBicycleForm" name="addBicycleForm" class="mx-auto" style="width: 300px">
                <div class="mb-3">
                    <label for="name" class="form-label">Name</label>
                    <input type="text" class="form-control" id="name">
                </div>
                <div class="mb-3">
                    <label for="wheelDiameter" class="form-label">WheelDiameter</label>
                    <input type="text" class="form-control" id="wheelDiameter">
                </div>
                <div class="mb-3">
                    <label for="color" class="form-label">Color</label>
                    <input type="text" class="form-control" id="color">
                </div>
                <div class="mb-3">
                    <label for="chain" class="form-label">Chain</label>
                    <input type="text" class="form-control" id="chain">
                </div>
                <div class="mb-3">
                    <label for="weight" class="form-label">Weight</label>
                    <input type="text" class="form-control" id="weight">
                </div>
                <div class="mb-3">
                    <label for="year" class="form-label">Year</label>
                    <input type="text" class="form-control" id="year">
                </div>
                <div class="mb-3">
                    <label for="price" class="form-label">Price</label>
                    <input type="text" class="form-control" id="price">
                </div>
                <input type="hidden" id="bicycleId" />
                <button type="submit" id="addBicycleBtn" class="btn btn-primary">Save</button>
            </form>`
}