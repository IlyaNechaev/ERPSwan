import './sidebar.css';
import React from 'react';

function NavBar(){

    let currentLocation = window.location.pathname;
    
    let handleLogout = function(){

    }

    return (
    <div className="sidebar-wrapper">
        <div className="header">
            <div className="title">Нечаев Илья</div>
        </div>
        <div className="body">
<<<<<<< Updated upstream
            <a href="/orders" className={"link" + (currentLocation == "/orders" ? " hl" : "")}>Заказы</a>
            <a href="/store" className={"link" + (currentLocation == "/store" ? " hl" : "")}>Склад</a>
            <a className="link">Выйти</a>
=======
            <a href="/swan/orders" className={"link" + (currentLocation == "/swan/orders" ? " hl" : "")}>Заказы</a>
            <a href="/swan/store" className={"link" + (currentLocation == "/swan/store" ? " hl" : "")}>Склад</a>
            <a className="link" onClick={handleLogout}>Выйти</a>
>>>>>>> Stashed changes
        </div>
    </div>
    )
}

export default NavBar;