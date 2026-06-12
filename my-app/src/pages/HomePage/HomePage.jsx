import React, { useEffect } from "react";
import styles from "./HomePage.module.scss";
import { useForm } from "react-hook-form";
import { useUpdateUser, useDeleteUser } from "../../hooks/useUserQueries";
import { useAuthStore } from "../../store/useAuthStore";
import { useNavigate } from "react-router-dom";
import LoadingSpinner from "../../components/LoadingSpinner/LoadingSpinner";
import PostCard from "../../components/PostCard/PostCard"
import { useGetPostsByUser, useGetUserFeed } from "../../hooks/usePostQueries";

const HomePage = () => {
  const navigate = useNavigate();
  const { user } = useAuthStore();
  const { data: feed, isLoading, isError } = useGetUserFeed();

  if (isLoading) return <LoadingSpinner />
  return (
    <div className={styles.homePage}>
      <div className={styles.content}>
        <h1>Home Page</h1>
        <div className={styles.feedWrapper}>
          {feed?.map((post) => (
            <PostCard key={post.id} post={post}/>
          ))}
        </div>
      </div>

      <div className={styles.suggestions}></div>
    </div>
  );
};

export default HomePage;
