// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('#personaNombre').change(function () {
        var criterios = $('#personaNombre').val();
        if (criterios.length >= 3) {
            var baseUrl = window.location.origin;
            var route = `${baseUrl}/api/EstudiantesApi?criterios=${criterios}`;
            $('#spinnerBusqueda').show();
            $.get(route, function (data) {
                var partialView = '';
                for (var i = 0; i < data.length; i++) {
                    partialView = partialView + '<li class="list-group-item">' +
                        `<span onclick = "setPersonaId(${data[i].personaId}, '${data[i].dataValueField}');">${data[i].dataValueField}</span>` +
                        '</li >';
                }
                $('#listaPersonas').html(partialView);
                $('#popupBusquedaPersona').show();
                $('#spinnerBusqueda').hide();
            });
        }
    });
});

function setPersonaId(id, valor) {
    $('#personaId').val(id);
    $('#personaNombre').val(valor);
    $('#popupBusquedaPersona').hide();
}