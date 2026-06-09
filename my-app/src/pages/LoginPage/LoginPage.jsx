import React, { useState } from "react";
import { useForm } from "react-hook-form";
import { useNavigate, Link } from "react-router-dom";
import { useLogin } from "../../hooks/useAuthQueries";
import LoadingSpinner from "../../components/LoadingSpinner/LoadingSpinner.jsx"
import styles from "./LoginPage.module.scss";

const LoginPage = () => {
  const { register, handleSubmit, formState } = useForm();
  const { mutate: login, isPending: isSaving, isError: isLoginError, error: loginError } = useLogin();
  const navigate = useNavigate();

  const onLogin = async (payload, e) => {
    e.preventDefault();
    login(payload, {
      onSuccess: () => {
        alert("Uspesno ste se prijavili!");
        navigate("/home");
      },
    });
  };

  if (isSaving) return <LoadingSpinner />;
  return(
    <div id="login-container">
      <h2>Login</h2>
      <form onSubmit={handleSubmit(onLogin)}>
        <div>
          <label>Username:</label>
          <input type="text" name="userName" {...register("userName")} />
        </div>
        <div>
          <label>Password:</label>
          <input type="password" name="password" {...register("password")} />
        </div>
        <button>Login</button>
      </form>
      <p className="forgot-password">
        <Link to="/forgot-password">Zaboravili ste lozinku?</Link>
      </p>
      {isLoginError && <p style={{ color: 'red' }}>{loginError.message}</p>}
    </div>
  );
};

export default LoginPage;