// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var page
var searchText
var year
var sortType

$('#bicyclesList').load('/Home/BicyclesList', loaded)

$('#search').on('click', function () {
    var searchText = $('#searchText').val()
    page = $('#Page').val()

    $('#bicyclesList').load(`/Home/BicyclesList/?search=${searchText}&page=1`, loaded)
})

$('#pages').children().on('click', function () {
    page = $('#Page').val();

    if (year == undefined) {
        year = "all"
    }

    switch (this.innerText) {
        case "Next":
            page++
            break
        case "Previous":
            page--
            break
        default:
            break
    }
    var queryStr = `/Home/BicyclesList/?search=&year=${year}&page=${page}`

    $('#bicyclesList').load(queryStr, loaded)
})

function loaded() {
    filterLoad()
    sortLoad()
}

function filterLoad() {
    $('#filter').on('click', function () {
        year = $('#year').val()

        $('#bicyclesList').load(`/Home/BicyclesList/?search=&year=${year}&page=1`, loaded)
    })
}

function sortLoad() {
    $('.sort').on('click', function () {
        page = $('#Page').val();
        sortType = `${this.innerText}Asc`.replace(" ", "")

        $('#bicyclesList').load(`/Home/BicyclesList/?sortType=${sortType}&search=&year=${year}&page=${page}`, loaded)
    })
}