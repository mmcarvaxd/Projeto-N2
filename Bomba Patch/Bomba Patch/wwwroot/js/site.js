﻿
function filtraTime() {
    var UsuarioId = $("#UsuarioId").val();
    var OrderBy = $("#OrderBy").val();
    var nomeTime = $("#NomeTime").val();
    var siglaTime = $("#SiglaTime").val();

    $.ajax({
        type: 'POST',
        url: "/Times/Filtra",
        cache: false,
        data: { "nomeTime": nomeTime, "siglaTime": siglaTime, "OrderBy": OrderBy, "UsuarioId": UsuarioId },
        success: function (dados) {
            $("#conteudoGrid").html(dados);
        }
    });
}



