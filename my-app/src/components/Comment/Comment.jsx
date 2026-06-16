import React from "react";
import styles from "./Comment.module.scss";
import Avatar from "../AvatarComponent/Avatar";

const Comment = ({ commentData, replyAction, parentId = null }) => {
  return (
    <div className={styles.comment}>
      <div className={styles.commentContent}>
        <Avatar avatar={commentData?.authorPictureUrl} />
        <p><b>{commentData.authorName}</b> {commentData.text}</p>
      </div>
      <div className={styles.commentFooter}>
        <button type="button" onClick={() => replyAction(parentId, commentData.authorName ?? commentData.authorName)}>Reply</button>
      </div>
      {commentData.replies && commentData.replies.length > 0 && (
        <div className={styles.repliesContainer}>
          {commentData.replies.map((reply) => (
            <Comment key={reply.id} commentData={reply} 
            parentId={parentId} replyAction={replyAction} />
          ))}
        </div>
      )}
    </div>
  );
};

export default Comment;