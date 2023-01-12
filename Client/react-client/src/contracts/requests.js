import CookieManager from "../utils/Cookie";

class RequestManager{
    
    static requestRoot = "https://localhost:7090/api/v1";

    // Home
    static loginUser = async function (login, password){
        let loginModel = {
            login: login,
            password: password
        };
        let headers = {
            'Content-Type': 'application/json'
        };

        let response = fetch(`${this.requestRoot}/login`, {
            method: 'POST',
            body: JSON.stringify(loginModel),
            headers: headers
        })
        .catch(err => console.log(err));
        
        return response;
    }

    // Store
    static getStoreList = function(name){
        let jwt = CookieManager.GetCookie('token');
        let response = fetch(`${this.requestRoot}/store/get-list${name == null ? '' : '?name=' + name}`, {
            method: "GET",
            headers:{
                'Authorization': jwt
            }
        })
        .catch(err => console.log(err));

        return response;
    }

    static getStore = function(id){
        let jwt = CookieManager.GetCookie('token');
        let response = fetch(`${this.requestRoot}/store/get?id=${id}`, {
            method: "GET",
            headers:{
                'Authorization': jwt
            }
        })
        .catch(err => console.log(err));

        return response;
    }

    // Order
    static getOrderList = function (number){
        let jwt = CookieManager.GetCookie('token');
        let response = fetch(`${this.requestRoot}/order/get-list${number == null ? '' : '?number=' + number}`, {
            method: "GET",
            headers:{
                'Authorization': jwt
            }
        });

        return response;
    }

    static getOrder = function(id){
        let jwt = CookieManager.GetCookie('token');
        let response = fetch(`${this.requestRoot}/order/get?id=${id}`, {
            method: "GET",
            headers:{
                'Authorization': jwt
            }
        });

        return response;
    }

    static approveOrder = function(id){
        let jwt = CookieManager.GetCookie('token');
        let response = fetch(`${this.requestRoot}/order/approve?id=${id}`, {
            method: "POST",
            headers:{
                'Authorization': jwt
            }
        });

        return response;
    }

    static completeOrder = function(id){
        let jwt = CookieManager.GetCookie('token');
        let response = fetch(`${this.requestRoot}/order/complete?id=${id}`, {
            method: "POST",
            headers:{
                'Authorization': jwt
            }
        });

        console.log(response);
        return response;
    }
}

export default RequestManager;