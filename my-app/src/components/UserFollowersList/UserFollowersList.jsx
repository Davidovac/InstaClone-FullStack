import React from 'react';
import styles from "./UserFollowersList.module.scss";
import { useAuthStore } from "../../store/useAuthStore";
import { useNavigate } from "react-router-dom";
import LoadingSpinner from "../../components/LoadingSpinner/LoadingSpinner";
import { useGetFollowersByUser } from '../../hooks/useUserQueries';
import Avatar from '../AvatarComponent/Avatar';

const UserFollowersList = ({ userId }) => {
  const navigate = useNavigate();
  const { user } = useAuthStore();
  const { data: followers, isLoading, isError } = useGetFollowersByUser(userId);
  
  if (isLoading) return <LoadingSpinner />
  if (followers?.length == 0) return <div className={styles.userFollowersList}><p>No followers</p></div>
  return(
    <div className={styles.userFollowersList}>
      {followers?.map((follower) => (
        <div key={follower.userName} className={styles.followEntry}>
          <Avatar avatar={follower.profilePictureUrl} />
          <p><b>{follower?.userName}</b></p>
        </div>
      ))}
    </div>
  )
}

export default UserFollowersList;