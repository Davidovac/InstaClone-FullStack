import React, { useState } from "react";
import styles from "./TabBar.module.scss";
import LoadingSpinner from "../LoadingSpinner/LoadingSpinner.jsx";
import UserPostsList from "../UserPostsList/UserPostsList.jsx";
import UserFollowersList from "../UserFollowersList/UserFollowersList.jsx";
import UserFollowingList from "../UserFollowingList/UserFollowingList.jsx";

const TabBar = ({ userId, postsCount = 0, followersCount = 0, followingCount = 0 }) => {
  const [activeTab, setActiveTab] = useState("posts") //"posts", "followers", "following"

  return (
    <div className={styles.tabBar}>
      <div className={styles.bars}>
        <div className={styles.postsTabHeading} onClick={() => setActiveTab("posts")}>Posts({postsCount})</div>
        <div className={styles.followersTabHeading} onClick={() => setActiveTab("followers")}>Followers({followersCount})</div>
        <div className={styles.followingTabHeading} onClick={() => setActiveTab("following")}>Following({followingCount})</div>
      </div>

      <div className={styles.tabContent}>
        {activeTab == "posts" && <UserPostsList userId={userId}/>}
        {activeTab == "followers" && <UserFollowersList userId={userId} />}
        {activeTab == "following" && <UserFollowingList userId={userId} />}
      </div>
    </div>
  );
}

export default TabBar;