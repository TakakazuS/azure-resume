window.addEventListener('DOMContentLoaded', (event) =>{
    getVisitCount();
})

const functionApiUrl = 'https://getvisitorcountertaka.azurewebsites.net/api/GetVisitorCounter?code=WTXu20Y_ibZQthrS34JF71Bm4J6OxbJcfskFfx2zxc-KAzFuqjQ2QA=='
const localFunctionApi = 'http://localhost:7071/api/GetVisitorCounter';

const getVisitCount = () => {
    let count = 30;
    fetch(functionApiUrl).then(response => {
        return response.json()
    }).then(response =>{
        console.log("Website called function API.");
        count = response.count;
        document.getElementById("counter").innerText = count;
    }).catch(function(error){
        console.log(error);
    });
    return count;
}