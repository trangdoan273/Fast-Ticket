function confirmDelete(userId, role) {
    if (role === 'admin') {
        if (confirm("Bạn có chắc chắn muốn xóa người dùng này?")) {
            window.location.href = `/Admin/DeleteUser/${userId}`;
        }
    } else {
        if (confirm("Bạn có chắc chắn muốn xóa người dùng này?")) {
            window.location.href = `/Admin/DeleteUser/${userId}`;
        }
    }
}