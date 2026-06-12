import React from "react";
import styles from "./Comment.module.scss";

const Comment = ({ commentData }) => {
  return (
    <div className={styles.comment}>
      <img className="profilePic" src={commentData?.authorPictureUrl && commentData?.authorPictureUrl.length > 10 
          ? `http://localhost:5231${commentData?.authorPictureUrl}` 
          : 'http://localhost:5231/images/defaults/defAvatar.jpg'} />
      <p><b>{commentData.authorName}</b> {commentData.text}</p>

      {commentData.replies && commentData.replies.length > 0 && (
        <div className="replies-container">
          {commentData.replies.map((reply) => (
            <Comment key={reply.id} commentData={reply} />
          ))}
        </div>
      )}
    </div>
  );
};

export default Comment;