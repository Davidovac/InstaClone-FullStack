import React, { useEffect, useState } from 'react';
import styles from "./ProfilePage.module.scss";
import { useAuthStore } from "../../store/useAuthStore";
import { useNavigate, useSearchParams } from "react-router-dom";
import LoadingSpinner from "../../components/LoadingSpinner/LoadingSpinner";
import { useFollowProfile, useFollowsProfile, useGetProfile, useUnfollowProfile } from '../../hooks/useUserQueries';
import TabBar from '../../components/TabBar/TabBar';

const ProfilePage = () => {
  const navigate = useNavigate();
  const { user } = useAuthStore();
  const [followed, setFollowed] = useState(false);
  const [searchParams] = useSearchParams();
  const userName = searchParams.get("userName");
  if (!userName) {
    navigate("/")
  }

  const { data: follows, isLoading: isChecking, isError: isErrorFollows } = useFollowsProfile(userName);
  const { data: profile, isLoading, isError: isErrorProfile } = useGetProfile(userName);
  const { mutate: follow, isPending: isSavingFollow, isError: isFollowError, error: followError } = useFollowProfile();
  const { mutate: unfollow, isPending: isDeletingFollow, isError: isUnfollowError, error: unfollowError } = useUnfollowProfile();
  
  const handleFollow = async () => {
    await follow(profile?.userName, {
      onSuccess: () => {
        setFollowed(true);
      },
    })
  }

  const handleUnfollow = async () => {
    await unfollow(profile?.userName, {
      onSuccess: () => {
        setFollowed(false);
      },
    })
  }

  useEffect(() => {
    if (follows) {
      setFollowed(follows);
    }
  },[follows])

  if (isLoading) return <LoadingSpinner />
  return(
    <div className={styles.profilePage}>
      <div className={styles.contentWrapper}>
        <div className={styles.profileInfo}>
          <img className={styles.profilePicBig} src={profile?.profilePictureUrl && profile?.profilePictureUrl.length > 10 
              ? `http://localhost:5231${profile?.profilePictureUrl}` 
              : 'http://localhost:5231/images/defaults/defAvatar.jpg'} />
          <div className={styles.details}>
            <p className={styles.userName}><b>{profile?.userName}</b></p>
            <p className={styles.description}>{profile?.description}</p>
          </div>
          <button type='button' onClick={() => followed ? handleUnfollow() : handleFollow()}>{followed ? "Unfollow" : "Follow"}</button>
        </div>
        <TabBar userId={profile.id} postsCount={profile?.postsCount} followersCount={profile?.followerCount} followingCount={profile?.followingCount}/>
      </div>
    </div>
  )
}

export default ProfilePage;