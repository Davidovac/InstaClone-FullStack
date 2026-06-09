import React, { useState } from "react";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { useSearchParams } from "react-router-dom";
import { useResetPassword } from "../../hooks/useAuthQueries";
import { LoadingSpinner } from "../../components/LoadingSpinner/LoadingSpinner";
import styles from "./ResetPasswordPage.module.scss";

const ResetPasswordPage = () => {
  const [searchParams] = useSearchParams();
  const token = searchParams.get("token");
  const email = searchParams.get("email");
  const { register, handleSubmit, formState } = useForm();
  const { mutate: resetPassword, isPending: isSaving, isError: isResetPassError, error: resetPassError } = useResetPassword();
  const navigate = useNavigate();

  const onResetPassword = async (data, e) => {
    e.preventDefault();
    const payload = { ...data, token, email };
    resetPassword(payload, {
      onSuccess: () => {
        alert("Uspesno ste resetovali lozinku!");
        navigate("/home");
      },
    });
  };

  if (isSaving) return <LoadingSpinner />;
  return(
    <div id="reset-password-container">
      <h2>Reset Password</h2>
      <form onSubmit={handleSubmit(onResetPassword)}>
        <div>
          <label>New Password:</label>
          <input type="password" name="newPassword" {...register("newPassword")} />

          <label>Confirm Password:</label>
          <input type="password" name="confirmPassword" {...register("confirmPassword")} />
        </div>
        <button>Reset Password</button>
      </form>
      {isResetPassError && <p style={{ color: 'red' }}>{resetPassError.message}</p>}
    </div>
  );
};

export default ResetPasswordPage;