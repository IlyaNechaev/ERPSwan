import { render } from "@testing-library/react";
import React from "react";
import { BrowserRouter, Routes, Route, Outlet } from 'react-router-dom';
import './pages/page.css';
import NavBar from './Navigation/NavBar';
import Orders from './pages/Orders';

function Account(){
    return (
        <div className="page-wrapper">
            <NavBar/>
            <Outlet/>
        </div>
    )
}

export default Account;