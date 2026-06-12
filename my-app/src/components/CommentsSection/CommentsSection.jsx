import React from "react";
import Comment from '../Comment/Comment';
import styles from './CommentsSection.module.scss';

const CommentsSection = ({ comments = [] }) => {
  return (
    <div className={styles.commentsSection}>
      {comments.length > 0 && comments.map((comment) => (
        <Comment key={comment.id} commentData={comment} />
      ))}
    </div>
  );
};

export default CommentsSection;