import React, { useState } from "react";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { useRegister } from "../../hooks/useAuthQueries";
import LoadingSpinner from "../../components/LoadingSpinner/LoadingSpinner";
import styles from "./RegisterPage.module.scss";

const RegisterPage = () => {
  const { register, handleSubmit, watch, formState: {errors, dirtyFields} } = useForm({
    mode: "onSubmit"
  });
  const { mutate: registerUser, isPending: isSaving, isError: isRegisterError, error: registerError } = useRegister();
  const navigate = useNavigate();
  const password = watch("password", "");

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


  if (isSaving) return <LoadingSpinner />;
  return(
    <div id="login-container">
      <h2>Register</h2>
      <form onSubmit={handleSubmit(onRegister)}>
        <div>
          <label>Username:</label>
          <input type="text" {...register("userName", { required: "Username is required"})} />
          {errors.userName && <p style={{ color: 'red' }}>{errors.userName.message}</p>}
        </div>
        <div>
          <label>email:</label>
          <input type="email" {...register("email", { required: "Email is required"})} />
          {errors.email && <p style={{ color: 'red' }}>{errors.email.message}</p>}
        </div>
        <div>
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
        </div>
        <div>
          <label>Confirm Password:</label>
          <input type="password" {...register("confirmPassword", {
            required: "Password confirmation is required",
            validate: value => value === password || "Passwords do not match"
          })} />
          {errors.confirmPassword && <p style={{ color: 'red' }}>{errors.confirmPassword.message}</p>}
        </div>
        <div>
          <label>First Name:</label>
          <input type="text" {...register("firstName", { required: "Name is required"})} />
          {errors.firstName && <p style={{ color: 'red' }}>{errors.firstName.message}</p>}
        </div>
        <div>
          <label>Last Name:</label>
          <input type="text" {...register("lastName", { required: "Last name is required"})} />
          {errors.lastName && <p style={{ color: 'red' }}>{errors.lastName.message}</p>}
        </div>
        {isRegisterError && <p style={{ color: 'red' }}>{registerError.message}</p>}
        <button type="submit">Register</button>
      </form>
    </div>
  );
};

export default RegisterPage;