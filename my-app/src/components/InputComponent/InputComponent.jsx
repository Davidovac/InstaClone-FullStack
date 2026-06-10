import React from "react";
import styles from "./InputComponent.module.scss";

const Input = ({
  iName,
  label,
  iType,
  labelShow = true,
  validateBool = false,
  validateType = "", //password, confirmPassword
  isRequired = true,
  register,
  errors,
  password,
}) => {
  
  const confirmPassValidation = {
    matchesPassword: value => value === password || "Passwords do not match",
  }

  const passwordValidation = {
    hasNumber: value => /\d/.test(value) || "Must contain a number",
    hasUpper: value => /[A-Z]/.test(value) || "Must contain an uppercase letter",
    hasLower: value => /[a-z]/.test(value) || "Must contain a lowercase letter",
    hasSpecial: value => /[!@#$%^&*(),.?":{}|<>]/.test(value) || "Must contain a special character",
  }

  const regulateValidationType = () => {
    if (validateType == "password") {
      return passwordValidation;
    }

    if (validateType == "confirmPassword") {
      return confirmPassValidation;
    }
    return;
  }
  
  const rules = {
    ...(isRequired && { required: `${label} is required` }),
    ...(validateBool && { validate: regulateValidationType() }),
  };

  return(
    <div className={styles.inputContainer}>
      {labelShow && <label>{label}</label>}
      <input type={iType} {...register(iName, rules)} />
      {errors?.[iName] && <p style={{ color: 'red' }}>{errors[iName].message}</p>}
    </div>
  );
}

export default Input;