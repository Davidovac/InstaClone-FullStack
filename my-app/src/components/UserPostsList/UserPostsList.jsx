import React from 'react';
import styles from "./UserPostsList.module.scss";
import { useAuthStore } from "../../store/useAuthStore";
import { useNavigate } from "react-router-dom";
import LoadingSpinner from "../../components/LoadingSpinner/LoadingSpinner";
import { useGetPostsByUser } from '../../hooks/usePostQueries';
import PostCard from '../PostCard/PostCard';

const UserPostsList = ({ userId }) => {
  const navigate = useNavigate();
  const { user } = useAuthStore();
  const { data: posts, isLoading, isError } = useGetPostsByUser(userId);
  
  if (isLoading) return <LoadingSpinner />
  return(
    <div className={styles.userPostsList}>
      {posts?.map((post) => (
        <PostCard key={post.id} post={post}/>
      ))}
    </div>
  )
}

export default UserPostsList;