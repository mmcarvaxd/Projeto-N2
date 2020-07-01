
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

function filtraPosicao() {
    var PePref = $("#PePref").val();
    var Nacionalidade = $("#Nacionalidade").val();
    var NomeJogador = $("#NomeJogador").val();
    var Posicao = $("#Posicao").val();
    var OrderBy = $("#OrderBy").val();

    $.ajax({
        type: 'POST',
        url: "/jogadores/Filtra",
        cache: false,
        data: {
            "PePref": PePref, "Nacionalidade": Nacionalidade,
            "NomeJogador": NomeJogador, "Posicao": Posicao, "OrderBy": OrderBy
        },
        success: function (dados) {
            $("#conteudoGrid").html(dados);
        }
    });
}



