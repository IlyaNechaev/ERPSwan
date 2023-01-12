import React from "react";
import '../../utils/utils.css';
import '../page.css';
import './order.css';
import RequestManager from "../../contracts/requests";
import { nullLiteral } from "@babel/types";

function Stage(stageData){
    let part = stageData.part;
    let isActive = stageData.isActive;
    let isHidden = stageData.isHidden;

    let handleComplete = async function(partId){
        await RequestManager.completeOrder(partId);
        window.location.reload();
    }

    let materials = part.storelist.map((mat, index) => {
        return (
            <div className="li" key={mat.id}>
                <div className="head">{index + 1}</div>
                <div className="content">{mat.name}</div>
                <div className="content">{mat.count}</div>
            </div>
        )
    });

    let dateEnd = null;
    let footer = null;

    if (!part.is_completed){
        if (isActive){
            footer = (
                <div className="footer">
                    <input type="button" className="btn btn-primary" value="Выполнить" onClick={() => { handleComplete(part.id); }}/>
                </div>
            );
        }
    }
    else{
        dateEnd = <div className="content">{part.date_end}</div>
        footer = (
            <div className="footer">
                <div className="row" style={{color: 'rgba(1,200,50,1)', fontWeight: 600}}>Выполнен</div>
            </div>
        );
    }

    return (
    <div className={"stage" + ((!isHidden || isActive) ? '' : ' hidden')} key={part.id}>
        <div className="header">Этап {part.order_num}</div>
        <div className="body">
            <div className="header">Материалы</div>
            <div className="list">
                {materials}
            </div>
        </div>
        {footer}
    </div>
);
}

export default Stage;