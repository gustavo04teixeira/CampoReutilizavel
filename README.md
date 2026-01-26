**🔍 Campo Reutilizável de Contribuinte com Autocomplete**

Componente reutilizável para consulta de contribuintes por CNPJ ou Nome Empresarial, com autocomplete em tempo real,
desenvolvido para aplicações ASP.NET WebForms, visando padronização, reutilização e redução de retrabalho em sistemas corporativos e públicos.

**🎯 Objetivo do Projeto**

O objetivo deste projeto é disponibilizar um campo de busca reutilizável, que possa ser facilmente integrado em diferentes telas de uma aplicação WebForms, 
eliminando a necessidade de recriar lógica, layout e regras de validação em cada novo módulo do sistema.

**🏛️ Contexto de Uso**

Projetado especialmente para sistemas públicos e corporativos, onde consultas recorrentes a contribuintes são comuns e a manutenção e padronização dos componentes é essencial.

**🚀 Funcionalidades**

-🔎 Busca por CNPJ ou Nome Empresarial

-⚡ Autocomplete em tempo real conforme digitação

-♻️ Campo reutilizável via UserControl (.ascx)

-🔗 Integração assíncrona com WebService ASMX

-✍️ Máscara automática de CNPJ

-🎨 Interface centralizada e responsiva

-🧩 Fácil integração em múltiplas páginas

**🛠️ Tecnologias Utilizadas**

**Tecnologia	- Versão**

**ASP.NET WebForm**s	- .NET Framework 4.7 / 4.8
**Linguagem**	- C#

**Web Service**	- ASMX

**JavaScript**	- Vanilla JS
**AJAX**	- jQuery

**Estilização** -	HTML, CSS, Bootstrap

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

IIS Express (padrão do Visual Studio)

**📥 Como Executar o Projeto**

1. Clone o repositório:

git clone https://github.com/gustavo04teixeira/CampoReutilizavel.git

2. Abra a solução no Visual Studio

3. Restaure os pacotes (se necessário)

4. Execute o projeto com IIS Express

**🔧 Como Utilizar o Campo Reutilizável**

1. Adicione o UserControl na página desejada:

<%@ Register Src="~/Controls/ContribuinteField.ascx" TagPrefix="uc" TagName="ContribuinteField" %>

2. Insira o componente no HTML da página:

<uc:ContribuinteField runat="server" />

3. Pronto! O campo já estará funcionando com autocomplete e máscara de CNPJ.

**🧠 Aprendizados e Desafios**

Durante o desenvolvimento deste projeto, foram explorados e consolidados conceitos como:

- Criação de componentes reutilizáveis em WebForms

- Comunicação assíncrona com ASMX via AJAX

- Manipulação dinâmica do DOM

- Máscaras de input em JavaScript

- Contorno de limitações do EventValidation do WebForms

- Organização de projetos para escalabilidade e manutenção

**⭐ Diferenciais do Projeto**

- Foco em reutilização de código

- Arquitetura simples e organizada

- Pronto para integração em sistemas reais

- Ideal para projetos WebForms legados ou em manutenção evolutiva

**👨‍💻 Autor**

**Gustavo Teixeira**  
Florianópolis – SC, Brasil  

- GitHub: https://github.com/gustavo04teixeira  
- LinkedIn: https://www.linkedin.com/in/gustavo-adolfo-teixeira-5a15311b2/
