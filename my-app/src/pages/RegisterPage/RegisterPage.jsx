import React, { useState } from "react";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { useRegister } from "../../hooks/useAuthQueries";
import "./RegisterPage.module.scss";

const RegisterPage = () => {
  const { register, handleSubmit, formState } = useForm();
  const { mutate: registerUser, isPending: isSaving, isError: isRegisterError, error: registerError } = useRegister();
  const navigate = useNavigate();
  const [isPasswordValidDeterminator, setIsPasswordValidDeterminator] = useState(true);

  const onRegister = async (data, e) => {
    const payload = {
      userName: data.userName,
      password: data.password,
      email: data.email,
      firstName: data.firstName,
      lastName: data.lastName,
    };
    e.preventDefault();
    if (isPasswordInvalid) {
      setIsPasswordValidDeterminator(false);
      return;
    }
    registerUser(payload, {
      onSuccess: () => {
        alert("Uspesno ste se registrovali!");
        navigate("/home");
      },
    });
  };

  

  const isPasswordMismatch = formState.dirtyFields.password && formState.dirtyFields.confirmPassword 
  && formState.values.password !== formState.values.confirmPassword;

  const isPasswordInvalid = formState.dirtyFields.password && 
  (formState.values.password.length < 8 || !/\d/.test(formState.values.password) || !/[A-Z]/.test(formState.values.password) 
  || !/[a-z]/.test(formState.values.password) || !/[!@#$%^&*(),.?":{}|<>]/.test(formState.values.password));


  if (isSaving) return <div id="loadingSpinner" className="spinner"></div>;
  return(
    <div id="login-container">
      <h2>Register</h2>
      <form onSubmit={handleSubmit(onRegister)}>
        <div>
          <label>Username:</label>
          <input type="text" name="userName" {...register("userName")} />
        </div>
        <div>
          <label>email:</label>
          <input type="email" name="email" {...register("email")} />
        </div>
        <div>
          <label>Password:</label>
          {!isPasswordValidDeterminator && <p style={{ color: 'red' }}>Password is invalid</p>}
          <input type="password" name="password" {...register("password", {onChange: () => setIsPasswordValidDeterminator(true),})} />
        </div>
        <div>
          <label>Confirm Password:</label>
          {isPasswordMismatch && <p style={{ color: 'red' }}>Passwords do not match</p>}
          <input type="password" name="confirmPassword" {...register("confirmPassword")} />
        </div>
        <div>
          <label>First Name:</label>
          <input type="text" name="firstName" {...register("firstName")} />
        </div>
        <div>
          <label>Last Name:</label>
          <input type="text" name="lastName" {...register("lastName")} />
        </div>
        <button type="submit" disabled={isPasswordMismatch}>Register</button>
      </form>
      {isRegisterError && <p style={{ color: 'red' }}>{registerError.message}</p>}
    </div>
  );
};

export default RegisterPage;