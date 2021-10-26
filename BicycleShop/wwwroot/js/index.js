
function createTableRow(bicycle) {
    const tr = document.createElement('tr')

    const titleTd = document.createElement('td')
    titleTd.append(bicycle.name)
    tr.append(titleTd)

    const colorTd = document.createElement('td')
    colorTd.append(bicycle.color)
    tr.append(colorTd)

    const priceTd = document.createElement('td')
    priceTd.append(bicycle.price)
    tr.append(priceTd)
        
    const deleteTd = document.createElement('td')
    deleteTd.append(createDeleteBtn(bicycle.id, "Delete"))
    deleteTd.append(createHidden(bicycle.id))
    tr.append(deleteTd)

    return tr
}

function createHidden(id) {

    var hidden = document.createElement('input')
    hidden.type = 'hidden'
    hidden.value = id

    return hidden
}

function createDeleteBtn(id, action) {
    var btn = document.createElement('button')
    btn.addEventListener('click', deleteEvent)
    btn.className = "btn btn-primary"
    btn.innerText = action
    return btn
}

async function getBicycles() {
    const response = await fetch('/api/bicycles')
    let rows = document.querySelector('tbody')
    rows.innerHTML = " "
    if (response.ok === true) {
        const bicycles = await response.json()

        let rows = document.querySelector('tbody')
        bicycles.forEach(bicycle => rows.append(createTableRow(bicycle)))
    }
}

function deleteEvent(e) {
    var id = getValue(this)
    var reference = `/api/bicycles/${id}`
    fetch(reference, {
        method: 'DELETE'
    }).then(getBicycles)
}

function getValue(btn) {
    var children = btn.parentElement.children
    var val
    for (var i = 0; i < children.length; i++) {
        if (children[i].type == 'hidden') {
            val = children[i].value
            break
        }
    }
    return val
}

getBicycles()
