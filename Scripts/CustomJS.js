$(document).ready(function () {

    let home = $("#NavHome")
    let category = $("#NavCategory")
    let archive = $("#NavArchive")
    let contact = $("#NavContact")

    $("#NavHome").click(function () {
        deselectAll()
        home.attr('class', 'nav-item active')
    })
    $("#NavJournal, #NavTutorials").click(function () {
        deselectAll()
        category.attr('class', 'nav-item submenu dropdown active')
    })
    $("#NavArchive").click(function () {
        deselectAll()
        archive.attr('class', 'nav-item active')
        console.log("This should be working")
    })
    $("#NavContact").click(function () {
        deselectAll()
        contact.attr('class', 'nav-item active')
    })

    function deselectAll() {
        home.attr('class', 'nav-item');
        category.attr('class', 'nav-item submenu dropdown');
        archive.attr('class', 'nav-item');
        contact.attr('class', 'nav-item');
    }


});