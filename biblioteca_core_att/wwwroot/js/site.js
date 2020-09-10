function validarForm(tipoForm) {

    var mensagem = "";
    var countErrors = 0;
    var form = document.getElementById("form");

    var nome = form.nome.value;

    //O NOME É UM CAMPO COMUM ENTRE AS CLASSES
    if (nome.indexOf(" ") == -1) {
        mensagem = mensagem + "- Informar nome válido. \n";
        countErrors++;
    }

    if (nome.trim() == "") {
        mensagem = mensagem + "- Informar um nome. \n";
        countErrors++;
    }

    if (tipoForm == "usuario") {
       return validarUsuario(mensagem, countErrors, form);
    } else if (tipoForm == "livro") {
        return validarLivro(mensagem, countErrors, form);
    }
}

function cpfFormat(field) {
    field.value = field.value.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/g, "\$1.\$2.\$3\-\$4");
}

function outFormat(field) {
    field.value = field.value.replace(/(\.|\/|\-)/g, "");
}

function validateEmail(email) {

    user = email.substring(0, email.indexOf("@"));
    domain = email.substring(email.indexOf("@") + 1, email.length);

    if ((user.length >= 1) && (domain.length >= 3) &&
        (user.search("@") == -1) && (domain.search("@") == -1) &&
        (user.search(" ") == -1) && (domain.search(" ") == -1) &&
        (domain.search(".") != -1) && (domain.indexOf(".") >= 1) &&
        (domain.lastIndexOf(".") < domain.length - 1)) {

        return true;
    }
    else {
        return false;
    }

}

function validarUsuario(mensagem, countErrors, form) {

    var email = form.email.value;
    var cpf = form.cpf.value;
    var idade = form.idade.value;

    if (email.trim() == "") {
        mensagem = mensagem + "- Informar um e-mail. \n";
        countErrors++;
    }

    if (!validateEmail(email)) {
        mensagem = mensagem + "- Informar um e-mail válido. \n";
        countErrors++;
    }

    if (cpf.trim() == "") {
        mensagem = mensagem + "- Informar cpf. \n";
        countErrors++;
    }

    if ((idade == null) || (idade < 18) || (idade > 99)) {
        mensagem = mensagem + "- Informar idade válida. \n";
        countErrors++;
    }

    if (countErrors > 0) {
        alert(mensagem);
        form.nome.focus();
        return false;
    } else {
        form.cpf.value = outFormat(cpf);
        return true;
    }
}

function validarLivro(mensagem, countErrors, form) {

    var autor = form.autor.value;
    var lancamento = form.lancamento.value;

    if (autor.indexOf(" ") == -1) {
        mensagem = mensagem + "- Informar um autor válido. \n";
        countErrors++;
    }

    if (autor.trim() == "") {
        mensagem = mensagem + "-Informar um autor. \n";
        countErrors++;
    }

    if (autor.match(/\d+/g) != null) {
        mensagem = mensagem + "-Não é permitido inserir números no nome do autor. \n";
        countErrors++;
    }

    if (lancamento.trim() == "") {
        mensagem = mensagem + "- Informar a data de lançamento completa. \n";
        countErrors++;
    }

    if (countErrors > 0) {
        alert(mensagem);
        form.nome.focus();
        return false;
    }
    return true;
}