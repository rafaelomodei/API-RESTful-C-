# Serviço/rest C#

Serviço RESTful para multiplataforma usando a API Web do ASP.NET Core com .NET Core e C#.
O serviço da suporte à adição, exibição, modificação e remoção, um uso padronizado de verbos de ação HTTP mais conhecido como CRUD (C riar, L er, A tualizar, E xcluir) de um cache na memória.

Esseprojeto foi baseado neste exemplo: [Criar uma API Web com o ASP.NET Core](https://docs.microsoft.com/pt-br/learn/modules/build-web-api-aspnet-core/)

### REST no ASP.NET Core

"Quando você navega para uma página da Web, o servidor Web se comunica com seus navegadores usando HTML, CSS e JavaScript. Se você interagir com a página fazendo algo como enviar um formulário de logon ou clicar em um botão de compra, o navegador enviará as informações de volta para o servidor Web.

De maneira semelhante, os servidores Web podem se comunicar com uma ampla gama de clientes, incluindo navegadores, dispositivos móveis, outros servidores Web e muito mais, usando serviços Web. Clientes de API se comunicam com o servidor por HTTP, e os dois trocam informações usando um formato de dados como JSON ou XML. Muitas vezes, as APIs são usadas em SPAs (aplicativos de página única) que executam a maior parte da lógica da interface do usuário em um navegador da Web, comunicando-se com o servidor Web usando principalmente APIs Web."

### REST: um padrão comum para criar APIs com HTTP

REST (Transferência de Estado Representacional) é um estilo de arquitetura para a criação de serviços Web. As solicitações REST são feitas por HTTP usando os mesmos verbos HTTP que os navegadores da Web usam para recuperar páginas da Web e enviar dados para os servidores. Os verbos são:

* **GET** – essa operação é usada para recuperar dados do serviço Web.

* **POST** – essa operação é usada para criar um item de dados no serviço Web.

* **PUT** – essa operação é usada para atualizar um item de dados no serviço Web.

* **PATCH** – essa operação é usada para atualizar um item de dados no serviço Web, descrevendo um conjunto de instruções sobre como o item deve ser modificado. Esse verbo não é usado no aplicativo de exemplo.

* **DELETE** – essa operação é usada para excluir um item de dados no serviço Web.
APIs de serviço Web que aderem ao REST são chamadas de APIs RESTful e são definidas usando:

Um URI de base.
Métodos HTTP, como GET, POST, PUT, PATCH ou DELETE.
Um tipo de mídia para os dados, como JSON (JavaScript Object Notation) ou XML.

## Pré-requisitos

* Ter instalado o [SDK do .NET Core](https://dotnet.microsoft.com/download) 

## Compilar o projeto de API Web

* Execute o comando a seguir para criar o aplicativo:

    ```$ dotnet build```

* Execute o seguinte comando da CLI do .NET Core no shell de comando:

    ```$ dotnet run```

* Abra um navegador da Web e navegue para:

    ```http://localhost:5000/product```

    A seguinte saída representa um trecho do JSON retornado:

    ```
    [
        {
            "id": 1,
            "name": "Caneta",
            "stock": 5,
            "preci": 1.5
        },
        {
            "id": 2,
            "name": "Lapis",
            "stock": 10,
            "preci": 1.1
        },
        {
            "id": 3,
            "name": "borracha",
            "stock": 20,
            "preci": 2.5
        }
    ] 
    ```
* Abra um novo terminal e execute o seguinte comando:

    ```$ dotnet tool install -g Microsoft.dotnet-httprepl```

    O comando anterior instala a ferramenta de linha de comando REPL (Read-Eval-Print Loop) HTTP do .NET que será usada para fazer solicitações HTTP para a API Web.

* Conecte-se à API Web executando o seguinte comando:

    ```$ httprepl http://localhost:5000```

* Navegue até o ponto de extremidade Product executando o seguinte comando:

    ```$ cd Product```

    O seguinte comando retorna as APIs disponíveis para o ponto de extremidade ```Product```:

    ```$ http://localhost:5000/> cd Product```

    ```$ /Product    [GET|POST]```

* Faça uma solicitação GET no HttpRepl usando o seguinte comando:
    
    ```$ get```

    O seguinte comando faz uma solicitação GET que é semelhante a navegar para o ponto de extremidade no navegador:

    ```
    http://localhost:5000/Product> get
    HTTP/1.1 200 OK
    Content-Type: application/json; charset=utf-8
    Date: Thu, 03 Jun 2021 20:26:46 GMT
    Server: Kestrel
    Transfer-Encoding: chunked

    [
        {
            "id": 1,
            "name": "Caneta",
            "stock": 5,
            "preci": 1.5
        },
        {
            "id": 2,
            "name": "Lapis",
            "stock": 10,
            "preci": 1.1
        },
        {
            "id": 3,
            "name": "borracha",
            "stock": 20,
            "preci": 2.5
        }
    ]
    ```

* Para consultar apenas um peoduto, podemos fazer outra solicitação ```GET```, mas passar um parâmetro id usando o seguinte comando:

    ```$ get 1```

    Isso retornará o Classic Italian com a seguinte saída:

    ``` 
    http://localhost:5000/Product> get 1
    HTTP/1.1 200 OK
    Content-Type: application/json; charset=utf-8
    Date: Thu, 03 Jun 2021 20:32:21 GMT
    Server: Kestrel
    Transfer-Encoding: chunked

    {
        "id": 1,
        "name": "Caneta",
        "stock": 5,
        "preci": 1.5
    }
    ```

* Nossa API também lida com situações em que o item não existe. Vamos chamar a API novamente, mas passar um produto inválido, id com o comando a seguir.

    ```$ get 5```

    Isso retornará um erro ```404 Not Found``` com a seguinte saída:

    ```
    http://localhost:5000/Product> get 5
    HTTP/1.1 404 Not Found
    Content-Type: application/problem+json; charset=utf-8
    Date: Thu, 03 Jun 2021 20:34:59 GMT
    Server: Kestrel
    Transfer-Encoding: chunked

    {
        "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
        "title": "Not Found",
        "status": 404,
        "traceId": "00-c4b120017191254b952ce4e8114c9e27-58557fbcb17de742-00"
    }
    ```

* Vamos atualizar o produto ```Lapis``` para ```Lapiseira``` com uma solicitação PUT com o seguinte comando:

    ```$ put 2 -c "{"id": 2, "name": "Lapiseira", "stock": 10, "preci": 2.80}" ```

    Isso retornará a seguinte saída de que foi bem-sucedido:

    ```
    http://localhost:5000/Product> put 2 -c "{"id": 2, "name": "Lapiseira", "stock": 10, "preci": 2.80}" 
    HTTP/1.1 204 No Content
    Date: Thu, 03 Jun 2021 20:41:43 GMT
    Server: Kestrel
    ```

    Para verificar se o produto foi atualizada, podemos executar novamente a ação GET com o seguinte comando:

    ```$ get 2```

    Isso retornará o produto recém-atualizada:

    ```
    http://localhost:5000/Product> get 2
    HTTP/1.1 200 OK
    Content-Type: application/json; charset=utf-8
    Date: Thu, 03 Jun 2021 20:44:42 GMT
    Server: Kestrel
    Transfer-Encoding: chunked

    {
        "id": 2,
        "name": "Lapiseira",
        "stock": 10,
        "preci": 2.8
    }
    ```

* Nossa API também pode excluir o produto recém-criado com a ação DELETE executando o seguinte comando:

    ```delete 2```

    Isso retornará um 204 No Content para êxito:

    ```
    http://localhost:5000/Product> delete 2
    HTTP/1.1 204 No Content
    Date: Thu, 03 Jun 2021 20:47:28 GMT
    Server: Kestrel
    ```

    Para verificar se o produto foi removida, podemos executar novamente a ação GET com o seguinte comando:

    ```$ get```

    Isso retornará os produtos originais como resultados:

    ```
    http://localhost:5000/Product> get
    HTTP/1.1 200 OK
    Content-Type: application/json; charset=utf-8
    Date: Thu, 03 Jun 2021 20:49:09 GMT
    Server: Kestrel
    Transfer-Encoding: chunked

    [
        {
            "id": 1,
            "name": "Caneta",
            "stock": 5,
            "preci": 1.5
        },
        {
            "id": 3,
            "name": "borracha",
            "stock": 20,
            "preci": 2.5
        }
    ]
    ```

* Saia da sessão de HttpRepl atual usando o seguinte comando:

    ```$ exit```