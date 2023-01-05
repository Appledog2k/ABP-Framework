using Acme.BookStore.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Acme.BookStore.Permissions;

public class BookStorePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        // Khi thêm câu lệnh này sẽ loại bỏ hết tất cả quyền xóa roles
    //    context
    //.GetPermissionOrNull(IdentityPermissions.Roles.Delete)
    //.IsEnabled = false;
        //var myGroup = context.AddGroup(BookStorePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(BookStorePermissions.MyPermission1, L("Permission:MyPermission1"));

        // Định nghĩa các nhóm quyền chỉ có thể chọn quyền con nếu quyền cha được chọn
        var bookStoreGroup = context.AddGroup(BookStorePermissions.GroupName, L("Permission:BookStore"));
        // Cài đặt quyền được áp dụng với
        var booksPermission = bookStoreGroup.AddPermission(BookStorePermissions.Books.Default, L("Permission:Books")
            ,multiTenancySide: MultiTenancySides.Host);
        // Có thể vô hiệu hóa quyền.
        booksPermission.AddChild(BookStorePermissions.Books.Create, L("Permission:Books.Create")
            ,isEnabled: true);
        booksPermission.AddChild(BookStorePermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(BookStorePermissions.Books.Delete, L("Permission:Books.Delete"));

        var authorsPermission = bookStoreGroup.AddPermission(
    BookStorePermissions.Authors.Default, L("Permission:Authors"));
        authorsPermission.AddChild(
            BookStorePermissions.Authors.Create, L("Permission:Authors.Create"));

        authorsPermission.AddChild(
            BookStorePermissions.Authors.Edit, L("Permission:Authors.Edit"));

        authorsPermission.AddChild(
            BookStorePermissions.Authors.Delete, L("Permission:Authors.Delete"));

        var productsPermission = bookStoreGroup.AddPermission(
    BookStorePermissions.Products.Default, L("Permission:Products"));
        productsPermission.AddChild(
            BookStorePermissions.Products.Create, L("Permission:Products.Create"));

        productsPermission.AddChild(
            BookStorePermissions.Products.Edit, L("Permission:Products.Edit"));

        productsPermission.AddChild(
            BookStorePermissions.Products.Delete, L("Permission:Products.Delete"));




    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BookStoreResource>(name);
    }
}
