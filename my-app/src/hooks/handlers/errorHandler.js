export function errorHandler(error) {
  const backendMessage = error.response?.status && error.response?.status != 500 ? error.response?.data?.detail : "Server error";
  console.error(backendMessage);
  return backendMessage;
}