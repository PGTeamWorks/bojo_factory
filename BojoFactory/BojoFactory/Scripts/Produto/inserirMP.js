$(document).ready(function () {
    carregarMateriaPrima();
    idMateriaPrimaChange();
});

function carregarMateriaPrima() {
    var select = $('#slc-materia-prima');
    var htmlOptions = '';

    $.getJSON('/MateriaPrima/Listar/', function (data) {
        $.each(data.materiasPrima, function (i, materiaPrima) {
            htmlOptions += '<option value="' + materiaPrima.Id + '">' + materiaPrima.Descricao + '</option>';
        });
        select.append(htmlOptions);
    });
}

function idMateriaPrimaChange() {
    $('#slc-materia-prima').on('change', function() {
        $('#idMp').val(this.value);
    });
}