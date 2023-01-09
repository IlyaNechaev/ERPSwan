class RequestManager{
    
    static requestRoot = "api/v1";

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
        let response = await fetch(`${this.requestRoot}/store/get-list${name == null ? '' : '?name=' + name}`, {
            method: "GET"
        })
        .catch(err => console.log(err));

        return response;
    }

    static getStore = async function(id){
        let response = await fetch(`${this.requestRoot}/store/get?id=${id}`, {
            method: "GET"
        })
        .catch(err => console.log(err));

        return response;
    }

    // Order
    static getOrderList = async function (number){
        let response = await fetch(`${this.requestRoot}/order/get-list${number == null ? '' : '?number=' + number}`, {
            method: "GET"
        })
        .catch(err => console.log(err));

        return response;
    }

    static getOrder = async function(id){
        let response = await fetch(`${this.requestRoot}/order/get?id=${id}`, {
            method: "GET"
        })
        .catch(err => console.log(err));

        return response;
    }

    static approveOrder = async function(partId){
        let response = await fetch(`${this.requestRoot}/order/approve?id=${partId}`, {
            method: "POST"
        })
        .catch(err => console.log(err));

        return response;
    }
}

export default RequestManager;