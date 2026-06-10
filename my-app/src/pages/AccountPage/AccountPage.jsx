import React, { use } from "react";
import styles from "./AccountPage.module.scss";
import { useForm } from "react-hook-form";
import { useUpdateUser, useDeleteUser } from "../../hooks/useUserQueries";
import { useAuthStore } from "../../store/useAuthStore";
import { useNavigate } from "react-router-dom";
import LoadingSpinner from "../../components/LoadingSpinner/LoadingSpinner";
import InputComponent from "../../components/InputComponent/InputComponent.jsx";

const AccountPage = () => {
  const navigate = useNavigate();
  const { user, setUser } = useAuthStore();
  const { mutate: updateUser, isPending: isSaving, isError: isUpdateError, error: updateError } = useUpdateUser();
  const { mutate: deleteUser, isPedning: isDeleteing, isError: isDeleteError, error: deleteError} = useDeleteUser();
  const { register, handleSubmit, watch, formState: { errors} } = useForm({
    defaultValues: {
      userName: user?.userName ?? "",
      email: user?.email ?? "",
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
        <InputComponent iName="userName" iType="text" label="Username"
          register={register}
          errors={errors}
        />

        <InputComponent iName="email" iType="email" label="Email"
          register={register}
          errors={errors}
        />
          
        <InputComponent iName="password" label="Password" iType="password"
          validateBool={true}
          validateType="password"
          register={register}
          errors={errors}
        />

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
