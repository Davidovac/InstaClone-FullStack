import React from "react";
import styles from "./InputComponent.module.scss";

const Input = ({
  iName,
  label,
  iType,
  labelShow = true,
  validateShow = false,
  validateObj = {},
  isRequired = true,
  register,
  errors,
}) => {
  const rules = {
    ...(isRequired && { required: `${label} is required` }),
    ...(validateShow && { validate: validateObj }),
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