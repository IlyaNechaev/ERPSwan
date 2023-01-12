import React from "react";
import '../utils/utils.css';
import './page.css';
import './order.css';
import RequestManager from "../contracts/requests";
import { nullLiteral } from "@babel/types";

function Order(){
    let [stages, setStages] = React.useState();
    const orderId = (new URLSearchParams(window.location.search)).get('id');

    let handleComplete = function(partId){
        RequestManager.approveOrder(partId);
        window.location.reload();
    }

    RequestManager.getOrder(orderId)
    .then((res) => res.json())
    .then((data) => {
        let newStages = data.parts.map((part) => {

            let materials = part.storelist.map((mat, index) => {
                return (
                    <div className="li">
                        <div className="head">{index + 1}</div>
                        <div className="content">{mat.name}</div>
                        <div className="content">{mat.count}</div>
                    </div>
                )
            });

            let dateEnd = null;
            let footer = null;
            if (!part.is_completed){
                footer = (
                    <div className="footer">
                        <input type="button" className="btn btn-primary" value="Выполнить" onClick={handleComplete(part.id)}/>
                    </div>
                );
            }
            else{
                dateEnd = <div className="content">{part.date_end}</div>
            }

            return (
            <div className={"stage" + part.is_completed ? '' : ' hidden'}>
                <div className="header">Этап {part.order_num}</div>
                <div className="body">
                    <div className="header">Материалы</div>
                    <div className="list">
                        {materials}
                    </div>
                </div>
                {footer}
            </div>
        );});

        setStages(newStages);
    });

    return (
        <div className="page-content">
            <div className="header">Производственный заказ 1</div>
            {stages}
        </div>
    );
}

export default Order;