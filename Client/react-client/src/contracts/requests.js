class RequestManager{
    
    static requestRoot = "https://11f90607-4757-429f-a1e0-ca85d98cfec2.mock.pstmn.io";

    // Home
    static loginUser = async function (login, password){
        let loginModel = {
            login: login,
            password: password
        };
        let headers = {
            'Content-Type': 'application/json'
        };

        let response = await fetch(`${this.requestRoot}/login`, {
            method: 'POST',
            body: JSON.stringify(loginModel),
            headers: headers
        })
        .catch(err => console.log(err));
        
        return response;
    }

    // Store
    static getStoreList = async function(name){
        let response = await fetch(`${this.requestRoot}/store/get-list?name=${name}`, {
            method: "GET"
        })
        .catch(err => console.log(err));

        return response.json();
    }

    static getStore = async function(id){
        let response = await fetch(`${this.requestRoot}/store/get?id=${id}`, {
            method: "GET"
        })
        .catch(err => console.log(err));

        return response.json();
    }

    // Order
    static getOrderList = async function (name){
        let response = await fetch(`${this.requestRoot}/order/get-list?name=${name}`, {
            method: "GET"
        })
        .catch(err => console.log(err));

        return response.json();
    }

    static getOrder = async function(id){
        let response = await fetch(`${this.requestRoot}/order/get?id=${id}`, {
            method: "GET"
        })
        .catch(err => console.log(err));

        return response.json();
    }
}

export default RequestManager;