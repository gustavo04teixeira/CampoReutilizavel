**🔍 Campo Reutilizável de Contribuinte com Autocomplete e Validação de CNPJ**

Componente reutilizável para consulta de contribuintes por CNPJ ou Nome Empresarial, com autocomplete em tempo real e validação oficial de CNPJ,
desenvolvido para aplicações ASP.NET WebForms, visando padronização, reutilização e redução de retrabalho em sistemas corporativos e públicos.

**🎯 Objetivo do Projeto**

-Disponibilizar um campo de busca reutilizável e inteligente, capaz de:

-Validar CNPJ (formato, existência e regra oficial)

-Consultar dados oficiais na Receita Federal

-Buscar contribuintes cadastrados localmente

-Padronizar layout, regras de negócio e comportamento

-Tudo isso em um único UserControl (.ascx) facilmente reutilizável em múltiplas telas.

**🏛️ Contexto de Uso**

-Projetado especialmente para sistemas públicos e corporativos, onde:

-Consultas a contribuintes são frequentes

-Regras de validação precisam ser consistentes

-Manutenção e reutilização de componentes são essenciais

-Projetos WebForms legados precisam evoluir sem reescrita completa

**🚀 Funcionalidades**

-🔎 Busca por CNPJ ou Nome Empresarial

-⚡ Autocomplete em tempo real conforme digitação

-♻️ Componente reutilizável via UserControl (.ascx)

-🔗 Integração assíncrona com WebService ASMX

-🌐 Validação de CNPJ via API pública oficial

-✅ Verificação de:

  -Formato do CNPJ

  -Dígitos verificadores

Existência na Receita Federal

-🛑 Mensagens claras de erro:

  -CNPJ inválido

  -CNPJ não encontrado

-✍️ Máscara automática de CNPJ

-🎨 Interface centralizada, padronizada e responsiva

-🧩 Fácil integração em múltiplas páginas

-🔄 Fluxo inteligente de validação (API → base local)

**🧠 Fluxo de Validação do CNPJ**

O componente segue o fluxo abaixo:

1️⃣ Usuário digita o CNPJ
2️⃣ Máscara é aplicada automaticamente
3️⃣ Consulta à API pública de CNPJ
4️⃣ Resultado:

-❌ 400 → CNPJ inválido

-❌ 404 → CNPJ não encontrado na Receita

-✅ 200 → CNPJ válido → consulta na base local

-5️⃣ Caso exista localmente, os dados são exibidos

**🛠️ Tecnologias Utilizadas**

**Tecnologia	- Versão**

**ASP.NET WebForms**	- .NET Framework 4.7 / 4.8

**Linguagem**	- C#

**Web Service**	- ASMX

**JavaScript**	- Vanilla JS

**AJAX**	- jQuery

**Estilização**	- HTML, CSS, Bootstrap

**Servidor**	- IIS Express

**IDE**	- Visual Studio

**📁 Estrutura do Projeto**

CampoReutilizavel

│

├── Content

│   └── css

│

├── Controls

│   └── ContribuinteField.ascx

│

├── Model

│   ├── Contribuinte.cs

│   └── ContribuinteRepository.cs

│

├── Pages

│   ├── App.aspx

│   └── SecondScreen.aspx

│

├── Scripts

│   └── CustomerScripts

│       └── contribuinte-field.js

│

├── Services

│   └── ContribuinteService.asmx

**⚙️ Instalação e Uso**

📌 Pré-requisitos

Visual Studio 2019 ou superior

.NET Framework 4.7 ou 4.8

IIS Express

**📥 Como Executar o Projeto**

git clone https://github.com/gustavo04teixeira/CampoReutilizavel.git

Abra a solução no Visual Studio

Restaure os pacotes (se necessário)

Execute o projeto com IIS Express

**🔧 Como Utilizar o Campo Reutilizável**

1️⃣ Registrar o UserControl

<%@ Register Src="~/Controls/ContribuinteField.ascx" 
    TagPrefix="uc"   
    TagName="ContribuinteField" %>

2️⃣ Inserir o componente na página
<uc:ContribuinteField runat="server" />

3️⃣ Pronto 🎉

O campo já estará funcionando com:

Autocomplete

Máscara de CNPJ

Validação oficial

Mensagens de erro inteligentes

**🧠 Aprendizados e Desafios**

Durante o desenvolvimento foram consolidados conceitos como:

-Criação de UserControls reutilizáveis em WebForms

-Comunicação assíncrona com ASMX via AJAX

-Integração com API pública de validação de CNPJ

-Manipulação dinâmica do DOM

-Máscaras de input em JavaScript

-Tratamento de status HTTP (200, 400, 404)

-Contorno das limitações do EventValidation do WebForms

-Organização de projeto para manutenção e escalabilidade

**⭐ Diferenciais do Projeto**

✔ Validação real de CNPJ (não apenas regex)

✔ Mensagens de erro claras e amigáveis

✔ Código reutilizável e desacoplado

✔ Arquitetura simples e organizada

✔ Pronto para uso em sistemas reais

✔ Ideal para WebForms legados ou manutenção evolutiva

**👨‍💻 Autor**

Gustavo Teixeira
Florianópolis – SC, Brasil

GitHub: https://github.com/gustavo04teixeira

LinkedIn: https://www.linkedin.com/in/gustavo-adolfo-teixeira-5a15311b2/
