import React, { useEffect, useRef, useState } from "react";
import styles from "./PostCard.module.scss";
import CommentsSection from "../CommentsSection/CommentsSection";
import PostEngagementDisplay from "../PostEngagementDisplay/PostEngagementDisplay.jsx";
import SendCommentInput from "../SendCommentInput/SendCommentInput";
import { useCreateComment, useLikePost, useUnlikePost } from "../../hooks/usePostQueries.js";

const PostCard = ({ post }) => {
  const [commentsShow, setCommentsShow] = useState(false);
  const [typedComment, setTypedSomment] = useState("");
  const { mutate: createComment, isPending: isSavingComment, isError: isCreateCommentError, error: createCommentError } = useCreateComment();
  const { mutate: like, isPending: isSavingLike, isError: isLikeError, error: likeError } = useLikePost();
  const { mutate: unlike, isPending: isDeletingLike, isError: isUnlikeError, error: enlikeError } = useUnlikePost();
  const [action, setAction] = useState(null); // "like" | "unlike" | null
  const timeoutRef = useRef(null);
  const [isActuallyLiked, setIsActuallyLiked] = useState(false);
  

  useEffect(() => {
    if (!post) return;

    if (post?.isLiked) {
      setIsActuallyLiked(true)
    }
    else if (!post?.isLiked) {
      setIsActuallyLiked(false)
    }
  },[post])

  useEffect(() => {
    if (!action) {
      if (timeoutRef.current) clearTimeout(timeoutRef.current);
      return;
    }

    if (timeoutRef.current) clearTimeout(timeoutRef.current);

    timeoutRef.current = window.setTimeout(async () => {
      if (action === "like") {
        await like(post.id), {
          onSuccess: () => {
            setIsActuallyLiked(true);
          }
        }
      }
      if (action === "unlike") {
        await unlike(post.id), {
          onSuccess: () => {
            setIsActuallyLiked(false);
          }
        }
      }
      setAction(null);
      timeoutRef.current = null;
    }, 3000);

    return () => {
      if (timeoutRef.current) clearTimeout(timeoutRef.current);
    };
  }, [action, like, unlike, post.id]);

  const handleLikeUnlike = () => {
    if (isActuallyLiked) {
      setIsActuallyLiked(false);
    }
    else {
      setIsActuallyLiked(true);
    }
    
    if (!action) {
      setAction(isActuallyLiked ? "unlike" : "like");
    } else {
      setAction(null); // Cancel pending action
    }
  };
  
  const handleSend = async () => {
    await createComment({ postId: post.id, data: typedComment }), {
      onSuccess: (data) => {
        setTypedSomment("");
        post.comments.push(data);
      }
    };
  }

  return(
    <div className={styles.postCard}>
      <div className={styles.header}>
        <img className="profilePic" src={post?.authorPictureUrl && post?.authorPictureUrl.length > 10 
          ? `http://localhost:5231${post?.authorPictureUrl}` 
          : 'http://localhost:5231/images/defaults/defAvatar.jpg'} />
        <p><b>{post.authorName}</b></p>
      </div>

      <div className={styles.imageWrapper}>
        <img src={`http://localhost:5231${post?.photo}`} />
      </div>

      <PostEngagementDisplay 
      setCommentsShow={setCommentsShow} 
      likesCount={post?.likes?.length}
      commentsCount={post?.comments?.length}
      commentsShow={commentsShow}
      authorUsername={post?.authorName}
      caption={post?.caption}
      isLiked={isActuallyLiked}
      onLikeUnlike={handleLikeUnlike}/>

      <div className={styles.commentsSectionContainer}>
        {commentsShow && <CommentsSection comments={post?.comments}/>}
        <SendCommentInput comment={typedComment} setComment={setTypedSomment} onSend={handleSend}/>
      </div>
    </div>
  );
}

export default PostCard;