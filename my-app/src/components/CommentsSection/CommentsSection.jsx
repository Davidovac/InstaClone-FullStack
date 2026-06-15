import React from "react";
import Comment from '../Comment/Comment';
import styles from './CommentsSection.module.scss';

const CommentsSection = ({ comments = [], replyAction }) => {
  return (
    <div className={styles.commentsSection}>
      {comments.length > 0 && comments.map((comment) => (
        <Comment key={comment.id} commentData={comment} replyAction={replyAction} parentId={comment.id} />
      ))}
    </div>
  );
};

export default CommentsSection;