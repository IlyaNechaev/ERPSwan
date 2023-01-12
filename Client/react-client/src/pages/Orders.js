import React from "react";
import '../utils/utils.css';
import './page.css';
import RequestManager from "../contracts/requests";

function Orders(){
    let [orderList, setOrderList] = React.useState();

    let handleAddOrder = function(e){
        window.location.href = '/swan/new-order';
    }

    React.useEffect(() => {
        RequestManager.getOrderList()
        .then((res) => res.json())
        .then((data) => {
            console.log(data);
            let newOrders = data.list.map((order, index) => {
                let isCompletedItem = null;
        
                if (order.is_completed){
                    isCompletedItem = <div className="content clr-green">Выполнен</div>
                }
                else{
                    isCompletedItem = <div className="content clr-red">Не выполнен</div>
                }
        
                return (
                    <div key={order.id} className="li">
                        <div className="head">{index + 1}</div>
                        <div className="content"><a href={'/swan/order?id='+order.id}>{"Производственный заказ " + order.number}</a></div>
                        {isCompletedItem}
                    </div>
                )
            });

            setOrderList(newOrders);
        });    
    }, []);

    return (
        <div className="page-content">
            <div className="row end">
                <input type="button" className="btn btn-primary" value="Добавить" onClick={handleAddOrder}/>
            </div>
            <div className="list">
                {orderList}
            </div>
        </div>
    );
}

export default Orders;