$(function () {
    $('#editWorker').on('click', '.editRow', function () {
        var id = parseInt($(this).find('.rowId').html());
        document.location.href = '/Worker/Edit?id=' + id;
    });
});

$(function () {
    $('#editCompany').on('click', '.editRow', function () {
        var id = parseInt($(this).find('.rowId').html());
        document.location.href = '/Company/Edit?id=' + id;
    });
});

function fillInputs(form) {
    let url = new URL(window.location.href);
    if (!url.search) return;
    for (let [name, value] of url.searchParams) {
        form.elements[name].value = value;
    }
}
fillInputs(document.forms.find);
