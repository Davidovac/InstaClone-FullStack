import React, { useState } from "react";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { useSearchParams } from "react-router-dom";
import { useResetPassword } from "../../hooks/useAuthQueries";
import LoadingSpinner from "../../components/LoadingSpinner/LoadingSpinner";
import InputComponent from "../../components/InputComponent/InputComponent.jsx";
import styles from "./ResetPasswordPage.module.scss";

const ResetPasswordPage = () => {
  const [searchParams] = useSearchParams();
  const token = searchParams.get("token");
  const email = searchParams.get("email");
  const { register, handleSubmit, watch, formState: {errors} } = useForm();
  const { mutate: resetPassword, isPending: isSaving, isError: isResetPassError, error: resetPassError } = useResetPassword();
  const navigate = useNavigate();
  const password = watch("newPassword");

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
    <div id={styles.resetPasswordContainer}>
      <h2>Reset Password</h2>
      <form onSubmit={handleSubmit(onResetPassword)}>
        <InputComponent iName="newPassword" label="New Password" iType="password"
          validateBool={true}
          validateType="password"
          register={register}
          errors={errors}
        />

        <InputComponent iName="confirmPassword" label="Confirm Password" iType="password"
          validateBool={true}
          validateType="confirmPassword"
          password={password}
          register={register}
          errors={errors}
        />
        <button type="submit">Reset Password</button>
      </form>
      {isResetPassError && <p style={{ color: 'red' }}>{resetPassError.message}</p>}
    </div>
  );
};

export default ResetPasswordPage;