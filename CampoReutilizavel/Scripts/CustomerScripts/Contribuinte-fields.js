document.addEventListener("DOMContentLoaded", function () {

    const input = document.getElementById("txtContribuinte");

    if (!input) return;

    input.addEventListener("input", function () {

        esconderMensagem();

        this.classList.remove("is-invalid");
        this.classList.remove("is-valid");

        this.value = aplicarMascaraCNPJ(this.value);

        const lbDuplicado = document.getElementById("lbMensagemCnpjDuplicado");
        if (lbDuplicado) lbDuplicado.style.display = "none";

        const lbAdicionado = document.getElementById("lbMensagemCnpjAdicionado");
        if (lbAdicionado) lbAdicionado.style.display = "none";

        const lbExcluido = document.getElementById("lbContribuinteExcluido");
        if (lbExcluido) lbExcluido.style.display = "none";

        const cnpjAlphanumericoLimpo = this.value.replace(/[^a-zA-Z0-9]/g, "");

        const sugestoes = document.getElementById("listaSugestoes");

        if (cnpjAlphanumericoLimpo.length === 14)
        {
            validarCNPJApi(cnpjAlphanumericoLimpo);
        } else if (cnpjAlphanumericoLimpo.length >= 3)
        {
            buscarContribuintes(this.value);
        }
        else
        {
            if (sugestoes) {
                sugestoes.innerHTML = "";
                sugestoes.style.display = "none";
            }
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
        success: async function (r) {
            //montarSugestoes(r.d);
            if (r.d && r.d.length > 0) {
                // Filtra a lista: só mantém quem a API validar
                const listaValidada = await filtrarApenasCnpjsValidos(r.d);
                montarSugestoes(listaValidada);
            } else {
                montarSugestoes([]);
            }
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
    const sugestoes = document.getElementById("listaSugestoes");

    fetch(`https://publica.cnpj.ws/cnpj/${cnpj}`)
        .then(response => {
            if (!response.ok) {
                
                if (sugestoes) {
                    sugestoes.innerHTML = "";
                    sugestoes.style.display = "none";
                }

                if (response.status === 400) throw new Error("CNPJ não é válido, tente novamente.");
                if (response.status === 404) throw new Error("CNPJ não encontrado, tente novamente.");
                throw new Error("Erro ao consultar a Receita Federal.");
            }
            return response.json();
        })
        .then(data => {
            $.ajax({
                url: '/Services/ContribuinteService.asmx/BuscarContribuinte',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({ termo: cnpj }), // busca o CNPJ específico
                dataType: 'json',
                success: function (r) {
                    
                    if (r.d && r.d.length > 0) {
                        montarSugestoes(r.d); // SÓ MOSTRA A LISTA AGORA
                        mostrarStatus(true);
                        esconderMensagem();
                    } else {
                        if (sugestoes) {
                            sugestoes.innerHTML = "";
                            sugestoes.style.display = "none";
                        }
                        mostrarStatus(false);
                        mostrarMensagem("CNPJ oficial, mas não cadastrado no banco local.");
                    }
                },
                error: function () {
                    mostrarStatus(false);
                    mostrarMensagem("Erro ao conectar com o banco local.");
                }
            });
        })
        .catch(error => {
            if (sugestoes) {
                sugestoes.innerHTML = "";
                sugestoes.style.display = "none";
            }
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

function importarArquivo() {
    const fileInput = document.getElementById('fileXml');
    const msg = document.getElementById('msgXml');

    if (fileInput.files.length === 0) {
        alert("Selecione um arquivo primeiro!");
        return;
    }

    const file = fileInput.files[0];
    const extensao = file.name.substring(file.name.lastIndexOf(''));
    const reader = new FileReader();

    reader.onload = function (e) {
        const base64 = e.target.result.split(',')[1]; 

        $.ajax({
            url: '/Services/ContribuinteService.asmx/UploadArquivo',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ base64Xml: base64, extensao: extensao }),
            success: function (r) {
                msg.innerText = r.d;
                msg.className = "alert alert-success";
                msg.style.display = "block";
                fileInput.value = "";
            },
            error: function () {
                msg.innerText = "Erro ao processar arquivo.";
                msg.className = "alert alert-danger";
                msg.style.display = "block";
            }
        });
    };
    reader.readAsDataURL(file);
}

async function filtrarApenasCnpjsValidos(listaOriginal) {

    const promessas = listaOriginal.map(async (contribuinte) => {
        try {

            const cnpjLimpo = contribuinte.CNPJ.replace(/[^0-9a-zA-Z]/g, "");
            const response = await fetch(`https://publica.cnpj.ws/cnpj/${cnpjLimpo}`);

            return response.ok ? contribuinte : null;
        } catch (e) {
            return null; 
        }
    });

    const resultados = await Promise.all(promessas);
    return resultados.filter(c => c !== null);
}