import React from "react";
import '../../utils/utils.css';
import '../page.css';
import './order.css';
import Stage from "./Stage";
import RequestManager from "../../contracts/requests";
import { nullLiteral } from "@babel/types";

function Order(){
    let [body, setBody] = React.useState();
    let [orderNumber, setNumber] = React.useState();

    const orderId = (new URLSearchParams(window.location.search)).get('id');
    let handleApprove = async function(){
        await RequestManager.approveOrder(orderId);
        window.location.reload();
    }
    
    React.useEffect(() => {
        RequestManager.getOrder(orderId)
        .then((res) => res.json())
        .then((order) => {
            let isPreviousCompleted = true;
            let newStages = order.parts.map((part) => {
                if(!part.is_completed && isPreviousCompleted && order.is_approved){
                    isPreviousCompleted = false;
                    return <Stage part={part} isActive={true} isHidden={false}/>
                }
                else{
                    return <Stage part={part} isActive={false} isHidden={!part.is_completed}/>
                }
            });
            
            if (!order.is_approved){
                newStages.push(<input type="button" className="btn btn-primary" value="Утвердить" onClick={handleApprove} />);
            }
            setBody(newStages);
            setNumber(order.number);
        })
    }, []);

    return (
        <div className="page-content">
            <div className="header">Производственный заказ {orderNumber}</div>
            {body}
        </div>
    );
}

export default Order;