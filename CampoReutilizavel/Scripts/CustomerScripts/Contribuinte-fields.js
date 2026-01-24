document.addEventListener("DOMContentLoaded", function () {

    const input = document.getElementById("txtContribuinte");
    const dropdown = document.getElementById("dpContribuintes");

    if (!input || !dropdown) return;

        input.addEventListener("input", function () {
            this.value = aplicarMascaraCNPJ(this.value);
            buscarContribuintes(this.value);
        });

    dropdown.addEventListener("input", function () {
        const selecionado = this.options[this.selectedIndex];

        if (selecionado.value) {
            input.value = aplicarMascaraCNPJ(selecionado.value);
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
            const lista = r.d;
            const sugestoes = document.getElementById("listaSugestoes");

            sugestoes.innerHTML = "";

            if (!lista || lista.length === 0) {
                sugestoes.style.display = "none";
                return;
            }

            lista.forEach(c => {
                const item = document.createElement("a");
                item.className = "list-group-item list-group-item-action";
                item.textContent = `${c.NomeEmpresarial} - ${c.CNPJ}`;

                item.addEventListener("click", function () {

                    const input = document.getElementById("txtContribuinte");
                    const select = document.getElementById("dpContribuintes");

                    input.value = c.CNPJ;

                    select.innerHTML = "";

                    const option = document.createElement("option");
                    option.value = c.CNPJ;
                    option.text = `${c.NomeEmpresarial} - ${c.CNPJ}`;
                    option.selected = true;

                    select.appendChild(option);

                    sugestoes.innerHTML = "";
                    sugestoes.style.display = "none";
                });


                sugestoes.appendChild(item);
            });

            sugestoes.style.display = "block";
        },
        error: function (xhr) {
            console.error(xhr.status, xhr.responseText);
        }
    });
}



