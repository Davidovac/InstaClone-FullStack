import React, { useState } from "react";
import styles from "./CreatePost.module.scss";
import { useForm, Controller } from "react-hook-form";
import { useUpdateUser, useDeleteUser } from "../../hooks/useUserQueries";
import { useNavigate } from "react-router-dom";
import { useCreatePost } from "../../hooks/usePostQueries.js"
import LoadingSpinner from "../../components/LoadingSpinner/LoadingSpinner";
import InputComponent from "../../components/InputComponent/InputComponent.jsx"

const CreatePostPage = () => {
  const navigate = useNavigate();
  const { register, handleSubmit, control, formState: {errors} } = useForm();
  const { mutate: createPost, isPending, isError, error } = useCreatePost();
  const [currentPhoto, setCurrentPhoto] = useState(null);

  const onSubmit = async (data) => {
    const formData = new FormData();
    if (!data.photo || data.photo == "") {
      alert('Select photo!')
      return;
    }
    
    formData.append("Caption", data.caption);
    formData.append("Photo", data.photo);

    createPost(formData, {
      onSuccess: () => {
        alert("Photo added!");
        navigate("/");
      },
    });
  }

  if (isPending) return <LoadingSpinner />

  return (
    <div className={styles.createPostContainer}>
      <form onSubmit={handleSubmit(onSubmit)}>
        <Controller name="photo" control={control} render={({ field }) => (
              <input type="file" onChange={(e) => field.onChange(e.target.files[0])}/>)}
        />

        <div className={styles.imageAdded}></div>

        <InputComponent iName="caption" label="Caption"
          iType="text"
          isRequired={false} 
          register={register}
          errors={errors} 
        />

        <button type="submit"></button>
      </form>
    </div>
  );
};

export default CreatePostPage;
