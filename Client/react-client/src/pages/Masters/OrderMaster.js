import React from "react";
import StageMaster from "./StageMaster";
import '../page.css';
import '../Views/order.css';
import '../../utils/utils.css';
import Stage from "../Views/Stage";

function OrderMaster(){
    return (
        <>
            <div className="page-content">
                <div className="header">Новый производственный заказ</div>
                <div className="row">
                    <input type="text" placeholder="Номер ПЗ" className="primary" />
                </div>
                <div className="row">
                    <input type="text" placeholder="Бригадир" className="primary" />
                </div>
                <div className="row" style={{justifyContent: 'space-between'}}>
                    <h3 style={{marginBottom: 'auto'}}>Этапы:</h3>
                    <input type="button" className="btn btn-primary" value="Добавить" />
                </div>
                <div className="list">
                    <div className="li">
                        <div className="head">1</div>
                        <div className="content">Этап 1</div>
                        <div className="content">6 050</div>
                    </div>
                    <div className="li">
                        <div className="head">2</div>
                        <div className="content">Этап 2</div>
                        <div className="content">5 800</div>
                    </div>
                </div>
            </div>
        </>
    )
}

export default OrderMaster;