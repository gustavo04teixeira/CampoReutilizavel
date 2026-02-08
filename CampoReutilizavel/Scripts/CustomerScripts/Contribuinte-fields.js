document.addEventListener("DOMContentLoaded", function () {

    const input = document.getElementById("txtContribuinte");

    if (!input) return;

    input.addEventListener("input", function () {

        this.value = aplicarMascaraCNPJ(this.value);

        const lbDup = document.getElementById("lbMensagemCnpjDuplicado");
        if (lbDup) lbDup.style.display = "none";

        const cnpjLimpo = this.value.replace(/\D/g, "");

        if (this.value.length >= 3) {
            buscarContribuintes(this.value);
        }

        if (cnpjLimpo.length === 14) {
            validarCNPJApi(cnpjLimpo);
        } else {
            const btn = document.getElementById("btnAdicionarLista");
            if (btn) btn.disabled = true;
        }
    });

});

function aplicarMascaraCNPJ(valor) {
    if (/[a-zA-Z]/.test(valor)) {
        return valor;
    }

    let digits = valor.replace(/\D/g, "");

    if (digits.length <= 14) {
        digits = digits.replace(/^(\d{2})(\d)/, "$1.$2");
        digits = digits.replace(/^(\d{2})\.(\d{3})(\d)/, "$1.$2.$3");
        digits = digits.replace(/\.(\d{3})(\d)/, ".$1/$2");
        digits = digits.replace(/(\d{4})(\d)/, "$1-$2");
    }
    return digits;
}

function buscarContribuintes(termo) {
    $.ajax({
        url: '/Services/ContribuinteService.asmx/BuscarContribuinte',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ termo: termo }),
        dataType: 'json',
        success: function (r) {
            montarSugestoes(r.d);
        },
        error: function () {
            console.log("Erro ao buscar contribuintes.");
        }
    });
}

function montarSugestoes(lista) {

    const sugestoes = document.getElementById("listaSugestoes");
    const input = document.getElementById("txtContribuinte");
    const select = document.getElementById("dpContribuintes");

    sugestoes.innerHTML = "";

    if (!lista || lista.length === 0) {
        sugestoes.style.display = "none";
        return;
    }

    lista.forEach(c => {

        const item = document.createElement("a");
        item.className = "list-group-item list-group-item-action";
        item.textContent = c.NomeEmpresarial + " - " + c.CNPJ;

        item.addEventListener("click", function () {

            input.value = c.CNPJ;

            const hf = document.getElementById("hfNomeEmpresa");
            if (hf) hf.value = c.NomeEmpresarial;

            select.innerHTML = "";

            const option = document.createElement("option");
            option.value = c.CNPJ;
            option.text = c.NomeEmpresarial + " - " + c.CNPJ;
            option.selected = true;

            select.appendChild(option);

            sugestoes.innerHTML = "";
            sugestoes.style.display = "none";

            const btn = document.getElementById("btnAdicionarLista");
            if (btn) btn.disabled = false;

            const lbDup = document.getElementById("lbMensagemCnpjDuplicado");
            if (lbDup) lbDup.style.display = "none";
        });

        sugestoes.appendChild(item);
    });

    sugestoes.style.display = "block";
}

function validarCNPJApi(cnpj) {

    fetch(`https://publica.cnpj.ws/cnpj/${cnpj}`)
        .then(response => {
            if (!response.ok) {

                if (response.status === 400) {
                    throw new Error("CNPJ não é válido, tente novamente.");
                }

                if (response.status === 404) {
                    throw new Error("CNPJ não encontrado, tente novamente.");
                }
                throw new Error("Erro ao consultar a Receita Federal");
            }
            return response.json();
        })
        .then(data => {
            console.log("CNPJ válido:", data);

            verificarCnpjLocal(cnpj);
        })

        .catch(error => {
            console.error(error.message);
            mostrarStatus(false);
            mostrarMensagem(error.message);
        });
}

function mostrarStatus(valido) {

    const input = document.getElementById("txtContribuinte");
    const btn = document.getElementById("btnAdicionarLista");

    if (valido) {
        input.classList.remove("is-invalid");
        input.classList.add("is-valid");
        if (btn) btn.disabled = false;
    } else {
        input.classList.remove("is-valid");
        input.classList.add("is-invalid");
        if (btn) btn.disabled = true;
    }
}
function mostrarMensagem(texto) {
    const msg = document.getElementById("mensagemCnpj");
    msg.innerText = texto;
    msg.style.display = "block";
}

function esconderMensagem() {
    const msg = document.getElementById("mensagemCnpj");
    msg.style.display = "none";
}

function verificarCnpjLocal(cnpj) {

    $.ajax({
        url: '/Services/ContribuinteService.asmx/BuscarContribuinte',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ termo: cnpj }),
        dataType: 'json',
        success: function (r) {

            if (!r.d || r.d.length === 0) {
                mostrarStatus(false);
                mostrarMensagem("CNPJ não encontrado, tente novamente.");
                return;
            }

            mostrarStatus(true);
            document.getElementById("mensagemCnpj").style.display = "none";
        },
        error: function () {
            mostrarStatus(false);
            mostrarMensagem("Erro ao consultar base local.");
        }
    });
}