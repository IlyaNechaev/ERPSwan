import './sidebar.css';
import React from 'react';
import CookieManager from '../utils/Cookie';
import { Navigate } from 'react-router';

function NavBar(){
    let currentLocation = window.location.pathname;
    let user = JSON.parse(CookieManager.GetCookie('swan-user'));
    
    let handleLogout = function(e){
        CookieManager.RemoveCookie('token');
        CookieManager.RemoveCookie('swan-user');
        window.location.href = '/login';
    }

    return (
    <div className="sidebar-wrapper">
        <div className="header">
            <div className="title">{user.lastName + ' ' + user.firstName}</div>
        </div>
        <div className="body">
            <a href="/swan/orders" className={"link" + (currentLocation == "/swan/orders" ? " hl" : "")}>Заказы</a>
            <a href="/swan/store" className={"link" + (currentLocation == "/swan/store" ? " hl" : "")}>Склад</a>
            <a className="link" onClick={handleLogout}>Выйти</a>
        </div>
    </div>
    )
}

export default NavBar;