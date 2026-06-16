import React, { useState } from "react";
import { Route, Routes, BrowserRouter } from "react-router-dom";
import "./styles/global.scss";
import Sidebar from "./components/Sidebar/Sidebar.jsx";
import LoginPage from "./pages/LoginPage/LoginPage.jsx";
import RegisterPage from "./pages/RegisterPage/RegisterPage.jsx";
import ActivateAccount from "./pages/ActivateAccount/ActivateAccount.jsx";
import ResetPasswordPage from "./pages/ResetPasswordPage/ResetPasswordPage.jsx";
import ForgotPasswordPage from "./pages/ForgotPasswordPage/ForgotPasswordPage.jsx";
import AccountPage from "./pages/AccountPage/AccountPage.jsx";
import { RequireAuth, RedirectIfAuthenticated } from "./components/RouteGuards.jsx";
import CreatePostPage from "./pages/CreatePostPage/CreatePost.jsx";
import HomePage from "./pages/HomePage/HomePage.jsx";

const App = () => {
  return (
    <BrowserRouter>
      <div id="main-container">
        <Sidebar/>

        <div className="content-wrapper">
          <Routes>
            <Route path="/" element={
              <RequireAuth>
                  <HomePage />
                </RequireAuth>
            } />

            <Route path="/login"
              element={
                <RedirectIfAuthenticated>
                  <LoginPage />
                </RedirectIfAuthenticated>
              }
            />

            <Route path="/register"
              element={
                <RedirectIfAuthenticated>
                  <RegisterPage />
                </RedirectIfAuthenticated>
              }
            />

            <Route path="/activate-account" 
              element={
                <RedirectIfAuthenticated>
                  <ActivateAccount />
                </RedirectIfAuthenticated>
              } 
            />

            <Route path="/forgot-password" 
              element={
                <RedirectIfAuthenticated>
                  <ForgotPasswordPage />
                </RedirectIfAuthenticated>
                } 
              />
              
            <Route path="/reset-password"
            element={
              <RedirectIfAuthenticated>
                <ResetPasswordPage />
              </RedirectIfAuthenticated>

            } 
            />

            <Route path="/account"
              element={
                <RequireAuth>
                  <AccountPage />
                </RequireAuth>
              }
            />

            <Route path="/create-post"
              element={
                <RequireAuth>
                  <CreatePostPage />
                </RequireAuth>
              }
            />
            
          </Routes>
        </div>
      </div>
    </BrowserRouter>
  );
};

export default App;