import React, { useState } from "react";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { useForgotPassword } from "../../hooks/useAuthQueries";
import { LoadingSpinner } from "../../components/LoadingSpinner/LoadingSpinner";
import styles from "./ForgotPasswordPage.module.scss";

const ForgotPasswordPage = () => {
  const { register, handleSubmit, formState } = useForm();
  const { mutate: forgotPassword, isPending: isSaving, isError: isForgotPassError, error: forgotPassError } = useForgotPassword();
  const navigate = useNavigate();

  const onForgotPassword = async (payload, e) => {
    e.preventDefault();
    forgotPassword(payload);
  };

  if (isSaving) return <LoadingSpinner />
  return(
    <div id="forgot-password-container">
      <h2>Forgot Password</h2>
      <form onSubmit={handleSubmit(onForgotPassword)}>
        <div>
          <label>Email:</label>
          <input type="email" name="email" {...register("email")} />
        </div>
        <button>Forgot Password</button>
      </form>
      {isForgotPassError && <p style={{ color: 'red' }}>{forgotPassError.message}</p>}
    </div>
  );
};

export default ForgotPasswordPage;