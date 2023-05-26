# Azure resume
My own azure resume, following [ACG project video](https://www.youtube.com/watch?v=ieYrBWmkfno).

# What I built
100% Azure-hosted version of my resume.\
You can view my azure resume page from [here](https://www.takakazu.me)

- Diagram
![diagram](https://user-images.githubusercontent.com/106180700/232945784-9eed8d37-e645-4706-8acd-2f02c55e7257.png)

# Prerequisites
- [Github account](https://github.com)
- [Azure account](https://azure.microsoft.com/en-ca/)
- [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli)
- [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Azure Functions Core Tools](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=v4%2Cmacos%2Ccsharp%2Cportal%2Cbash#install-the-azure-functions-core-tools)
- [Visual Studio Code](https://code.visualstudio.com)
- [Visual Studio Code Extentions](https://code.visualstudio.com/docs/introvideos/extend)
  - [Azure Functions Extention](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azurefunctions)
  - [C# Extention](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
  - [Azure Storage Extention](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azurestorage)

# Frontend
The frontend is a static website with HTML, CSS and JavaScript.
- "[index.html](https://github.com/TakakazuS/azure-resume/blob/main/frontend/index.html)" file contains the content of the website.
- "[main.js](https://github.com/TakakazuS/azure-resume/blob/main/frontend/main.js)" file contains visitor counter code which fetches the data from function API.
- <[How to deploy static site to blob storage.](https://learn.microsoft.com/en-us/azure/storage/blobs/storage-blob-static-website-host)>

# Backend
The backend is an [HTTP triggered Azure Functions](https://learn.microsoft.com/en-us/azure/azure-functions/functions-bindings-http-webhook-trigger?tabs=python-v2%2Cin-process%2Cfunctionsv2&pivots=programming-language-csharp) with Cosmos DB input and output binding.
- Cosmos DB stores visiter counter data.
  - <[How to create an Azure Cosmos DB](https://learn.microsoft.com/en-us/azure/cosmos-db/nosql/quickstart-portal)>
- "[api](https://github.com/TakakazuS/azure-resume/tree/main/backend/api)" folder contains the function
  - When the Function is triggered, it retrieves the Cosmos DB visitor counter data, returns its value to the caller, and adds 1 to it, and sends it back to the Cosmos DB.
    - <[How to create an HTTP triggered Azure Function in Visual Studio Code.](https://learn.microsoft.com/en-us/azure/azure-functions/functions-develop-vs-code?tabs=csharp)>
    - <[Azure Functions Cosmos DB bindings](https://learn.microsoft.com/en-us/azure/azure-functions/functions-bindings-cosmosdb-v2?tabs=in-process%2Cfunctionsv2&pivots=programming-language-csharp)>
    - <[How to retrieve a Cosmos DB item with Functions binding.](https://learn.microsoft.com/en-us/azure/azure-functions/functions-bindings-cosmosdb-v2-input?tabs=python-v2%2Cin-process%2Cfunctionsv2&pivots=programming-language-csharp)>
    - <[How to write to a Cosmos DB item with Functions binding.](https://learn.microsoft.com/en-us/azure/azure-functions/functions-bindings-cosmosdb-v2-output?tabs=python-v2%2Cin-process%2Cfunctionsv2&pivots=programming-language-csharp)>


# Deploying to Azure 
1. Deploy the Azure Function to Azure.
   - [Publish Azure Function to Azure in Visual Studio Code.]((https://learn.microsoft.com/en-us/azure/azure-functions/functions-develop-vs-code?tabs=csharp#enable-publishing-with-advanced-create-options))
     - [Create a new Azure storage account.](https://github.com/microsoft/vscode-azurestorage)
     - Select the same resource group as for Cosmos DB.
   - [Edit Azure Functions application settings with the Cosmos DB connection string.](https://learn.microsoft.com/en-us/azure/azure-functions/functions-add-output-binding-cosmos-db-vs-code?tabs=in-process%2Cv1&pivots=programming-language-csharp#update-your-function-app-settings)
     - You can use [Azure Key Vault](https://learn.microsoft.com/en-us/azure/key-vault/general/basic-concepts) to store them securely.
   - Get Function URL.
     - Add URL into "main.js" for visitor counter.
   - [Enable Cross-Origin Resource Sharing (CORS) for the Function in Azure.](https://learn.microsoft.com/en-us/azure/cosmos-db/nosql/how-to-configure-cross-origin-resource-sharing)
2. [Deploy static site to blob container.](https://learn.microsoft.com/en-us/azure/storage/blobs/storage-blob-static-website)
   - Make sure Azure Storage Extention is installed.
   - Deploy "frontend" folder to Static Website via Azure Storage.
     - Create a new Azure storage account.
     - Select the same resource group as for Cosmos DB.
   - Add the URL of the website for Allowed Origins of CORS.
3. [Setup Azure Content Delivery Network (CDN) for HTTPS and custom domain support.](https://learn.microsoft.com/en-us/azure/cdn/cdn-custom-ssl?tabs=option-1-default-enable-https-with-a-cdn-managed-certificate)
   - [Create Azure CDN profile and endpoint](https://learn.microsoft.com/en-us/azure/cdn/cdn-create-new-endpoint) for the website.
     - Select Static Website not Blob for origin hostname.
   - Map custom domain to the Azure CDN profile.
     - *I used [NameCheap](https://www.namecheap.com) for my custom domain.
   - Enable custom domain HTTPS.
   - Add the custom domain URL of the website for Allowed Origins of CORS.

# Continuous Integration and Continuous Delivery (CI/CD) pipeline
With CI/CD, when you commit the code either from frontend or backend directory, it will get automatically pushed and deployed to the correct service.
- Create frontend workflow.
  - "[frontend.main.yml](https://github.com/TakakazuS/azure-resume/blob/main/.github/workflows/frontend.main.yml)" file contains the frontend workflow.
    - <[How to deploy static website in Azure Storage with Github Actions](https://learn.microsoft.com/en-us/azure/storage/blobs/storage-blobs-static-site-github-actions?tabs=userlevel)>
- Implement unit testing.
  - "[tests](https://github.com/TakakazuS/azure-resume/tree/main/backend/tests)" folder contains files for testing.
    - <[How to setup Xunit with Azure Functions](https://www.madebygps.com/how-to-use-xunit-with-azure-functions/)>
    - <[Reference to testing Azure Functions](https://learn.microsoft.com/en-us/azure/azure-functions/supported-languages)>
- Create backend workflow.
  - "[backend.main.yml](https://github.com/TakakazuS/azure-resume/blob/main/.github/workflows/backend.main.yml)" file contains the backend workflow.
    - <[How to deploy code to Function app in Azure with Github Actions](https://learn.microsoft.com/en-us/azure/azure-functions/functions-how-to-github-actions?tabs=dotnet#deploy-the-function-app)>