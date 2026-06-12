import React from "react";
import Comment from '../Comment/Comment.jsx';
import styles from './PostEngagementDisplay.module.scss';

const PostEngagementDisplay = ({ commentsShow, setCommentsShow, likesCount = 0, commentsCount = 0,
   authorUsername ="", caption = "", onLikeUnlike, isLiked = false }) => {
  return (
    <div className={styles.postEngagementDisplay}>
      <div className={styles.engagementBar}>
        <button type="button" className={`${styles.likebtn} ${isLiked ? styles.likedPostButton : ""}`} 
        onClick={onLikeUnlike}>L</button>
      </div>

      <div className={styles.captionRow}>
        {authorUsername && authorUsername != "" && <p><b>{authorUsername}</b> {caption}</p>}
      </div>

      {commentsCount > 0 && !commentsShow && <div className={styles.showCommentsContainer}>
        <button type="button" onClick={(e) => setCommentsShow(true)} className={styles.showCommentsBtn}>
          <b>Show {commentsCount} comments</b>
        </button>
      </div>}
    </div>
  );
};

export default PostEngagementDisplay;