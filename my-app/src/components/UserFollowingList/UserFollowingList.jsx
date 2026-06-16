import React, { forwardRef } from 'react';
import styles from "./UserFollowingList.module.scss";
import { useAuthStore } from "../../store/useAuthStore";
import { useNavigate } from "react-router-dom";
import LoadingSpinner from "../../components/LoadingSpinner/LoadingSpinner";
import { useGetFollowingByUser } from '../../hooks/useUserQueries';
import Avatar from '../AvatarComponent/Avatar';

const UserFollowingList = ({ userId }) => {
  const navigate = useNavigate();
  const { user } = useAuthStore();
  const { data: followings, isLoading, isError } = useGetFollowingByUser(userId);
  
  if (isLoading) return <LoadingSpinner />
  if (followings?.length == 0) return <div className={styles.userFollowingList}><p>No following</p></div>
  return(
    <div className={styles.userFollowingList}>
      {followings?.map((following) => (
        <div key={following.userName} className={styles.followingEntry}>
          <Avatar avatar={following.profilePictureUrl} />
          <p><b>{following?.userName}</b></p>
        </div>
      ))}
    </div>
  )
}

export default UserFollowingList;