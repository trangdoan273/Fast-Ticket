function confirmDelete(infoId, role) {
    if (role === 'admin') {
        if (confirm("Bạn có chắc chắn muốn xóa bài đăng này?")) {
            window.location.href = `/Admin/DeletePost/${infoId}`; // Sửa ở đây
        }
    } else {
        if (confirm("Bạn có chắc chắn muốn xóa bài đăng này?")) {
            window.location.href = `/Admin/DeletePost/${infoId}`; // Sửa ở đây
        }
    }
}
