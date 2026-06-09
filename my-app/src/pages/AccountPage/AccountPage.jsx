import React, { use } from "react";
import styles from "./AccountPage.module.scss";
import { useForm } from "react-hook-form";
import { useUpdateUser, useDeleteUser } from "../../hooks/useUserQueries";
import { useAuthStore } from "../../store/useAuthStore";
import { useNavigate } from "react-router-dom";
import LoadingSpinner from "../../components/LoadingSpinner/LoadingSpinner";

const AccountPage = () => {
  const navigate = useNavigate();
  const { user, setUser } = useAuthStore();
  const { mutate: updateUser, isPending: isSaving, isError: isUpdateError, error: updateError } = useUpdateUser();
  const { mutate: deleteUser, isPedning: isDeleteing, isError: isDeleteError, error: deleteError} = useDeleteUser();
  const { register, handleSubmit, watch, formState: { errors} } = useForm({
    defaultValues: {
      userName: user?.userName || "",
      email: user?.email || "",
    }
  });

  const onUpdate = async (data) => {
    const payload = {
      ...data,
      id: user?.id,
    }

    await updateUser(payload, {
      onSuccess: (data) => {
        alert('Data changed successfully.');
        setUser(data);
      }
    })
  }

  const handleDelete = () => {
    deleteUser(user.id, {
      onSuccess: () => {
        alert('User has been deleted successfully.');
        logoutUser();
      }
    })
  }

  const logoutUser = () => {
    useAuthStore.getState().logout();
    navigate("/login");
  };

  const password = watch("password", "");

  if (isSaving || isDeleteing) return <LoadingSpinner />

  return (
    <div className={styles.accountPage}>
      <div className={styles.hero}></div>
      <h1>Account</h1>
      <p>Welcome to your account page.</p>
      <form className={styles.userInfo} onSubmit={handleSubmit(onUpdate)}>
        <input type="text" {...register('userName', { required: 'Ovo polje je obavezno'})}/>
        {errors.userName && <p style={{ color: 'red' }}>{errors.userName.message}</p>}
        <input type="email"{...register('email', { required: 'Ovo polje je obavezno'})}/>
        {errors.email && <p style={{ color: 'red' }}>{errors.email.message}</p>}
        <input type="password" {...register("password", {
          validate: (value) => {
              if (!value) return true;
              if (value.length < 8) return "Minimum 8 characters";
              if (!/\d/.test(value)) return "Must contain a number";
              if (!/[A-Z]/.test(value)) return "Must contain an uppercase letter";
              if (!/[a-z]/.test(value)) return "Must contain a lowercase letter";
              if (!/[!@#$%^&*(),.?\":{}|<>]/.test(value)) return "Must contain a special character";
              return true;
            }
          })} />
        {errors.password && <p style={{ color: 'red' }}>{errors.password.message}</p>}

        {isUpdateError && <p style={{ color: 'red' }}>{updateError.message}</p>}
        {isDeleteError && <p style={{ color: 'red' }}>{deleteError.message}</p>}
        <div className={styles.buttonsContainer}>
          <button className={styles.buttonSubmit} type="submit">Update Info</button>
          <button className={styles.buttonDelete} type="button" onClick={handleDelete}>Delete Account</button>
        </div>
      </form>
    </div>
  );
};

export default AccountPage;
