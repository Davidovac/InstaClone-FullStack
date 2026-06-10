import React, { useState } from "react";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { useRegister } from "../../hooks/useAuthQueries";
import LoadingSpinner from "../../components/LoadingSpinner/LoadingSpinner";
import styles from "./RegisterPage.module.scss";
import InputComponent from "../../components/InputComponent/InputComponent.jsx"

const RegisterPage = () => {
  const { register, handleSubmit, watch, formState: {errors, dirtyFields} } = useForm({
    mode: "onSubmit"
  });
  const { mutate: registerUser, isPending: isSaving, isError: isRegisterError, error: registerError } = useRegister();
  const navigate = useNavigate();
  const password = watch("password");

  const onRegister = async (data) => {
    const payload = {
      userName: data.userName,
      password: data.password,
      email: data.email,
      firstName: data.firstName,
      lastName: data.lastName,
    };

    registerUser(payload, {
      onSuccess: () => {
        alert("Uspesno ste se registrovali!");
        navigate("/home");
      },
    });
  };

  const confirmPassValidate = {
    matchesPassword: value => value === password || "Passwords do not match",
  }

  const passwordValidation = {
    hasNumber: value => /\d/.test(value) || "Must contain a number",
    hasUpper: value => /[A-Z]/.test(value) || "Must contain an uppercase letter",
    hasLower: value => /[a-z]/.test(value) || "Must contain a lowercase letter",
    hasSpecial: value => /[!@#$%^&*(),.?":{}|<>]/.test(value) || "Must contain a special character",
  }


  if (isSaving) return <LoadingSpinner />;
  return(
    <div id="login-container">
      <h2>Register</h2>
      <form onSubmit={handleSubmit(onRegister)}>
        <InputComponent iName="userName" label="Username" iType="text"
          register={register}
          errors={errors}
        />
        <InputComponent iName="email" iType="email" label="Email"
          register={register}
          errors={errors}
        />
        {/*<div>
          <label>Password:</label>
          <input type="password" {...register("password", {
            required: "Password is required",
            minLength: { value: 8, message: "Minimum 8 characters"},
            validate: {
              hasNumber: value => /\d/.test(value) || "Must contain a number",
              hasUpper: value => /[A-Z]/.test(value) || "Must contain an uppercase letter",
              hasLower: value => /[a-z]/.test(value) || "Must contain a lowercase letter",
              hasSpecial: value => /[!@#$%^&*(),.?":{}|<>]/.test(value) || "Must contain a special character",
            },
          })} />
          {errors.password && <p style={{ color: 'red' }}>{errors.password.message}</p>}

        </div>*/}
        <InputComponent iName="password" label="Password" iType="password"
          validateShow={true}
          validateObj={passwordValidation}
          register={register}
          errors={errors}
        />

        <InputComponent iName="confirmPassword" label="Confirm Password" iType="password"
          validateShow={true}
          validateObj={confirmPassValidate}
          register={register}
          errors={errors}
        />

        <InputComponent iName="firstName" label="First Name" iType="text"
          register={register}
          errors={errors}
        />

        <InputComponent iName="lastName" label="Last Name" iType="text"
          register={register}
          errors={errors}
        />
        {isRegisterError && <p style={{ color: 'red' }}>{registerError.message}</p>}
        <button type="submit">Register</button>
      </form>
    </div>
  );
};

export default RegisterPage;