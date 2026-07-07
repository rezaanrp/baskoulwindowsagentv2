using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Identity.Commands
{
    public sealed record ChangeCurrentUserPasswordCommand(
        string UserId,
        string CurrentPassword,
        string NewPassword) : IRequest<ChangeCurrentUserPasswordResult>;

    public sealed record ChangeCurrentUserPasswordResult(
        bool Succeeded,
        IReadOnlyList<string> Errors);

    public sealed class ChangeCurrentUserPasswordCommandHandler
        : IRequestHandler<ChangeCurrentUserPasswordCommand, ChangeCurrentUserPasswordResult>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public ChangeCurrentUserPasswordCommandHandler(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ChangeCurrentUserPasswordResult> Handle(
            ChangeCurrentUserPasswordCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return new ChangeCurrentUserPasswordResult(false, new[] { "کاربر پیدا نشد." });
            }

            var result = await _userManager.ChangePasswordAsync(
                user,
                request.CurrentPassword,
                request.NewPassword);

            if (!result.Succeeded)
            {
                return new ChangeCurrentUserPasswordResult(
                    false,
                    result.Errors.Select(ToPersianMessage).ToList());
            }

            await _signInManager.RefreshSignInAsync(user);
            return new ChangeCurrentUserPasswordResult(true, Array.Empty<string>());
        }

        private static string ToPersianMessage(IdentityError error)
        {
            return error.Code switch
            {
                nameof(IdentityErrorDescriber.PasswordTooShort) => "پسورد باید حداقل ۶ کاراکتر باشد.",
                nameof(IdentityErrorDescriber.PasswordRequiresNonAlphanumeric) => "پسورد باید حداقل یک کاراکتر غیرحرفی یا غیرعددی داشته باشد.",
                nameof(IdentityErrorDescriber.PasswordRequiresLower) => "پسورد باید حداقل یک حرف کوچک انگلیسی داشته باشد.",
                nameof(IdentityErrorDescriber.PasswordRequiresUpper) => "پسورد باید حداقل یک حرف بزرگ انگلیسی داشته باشد.",
                nameof(IdentityErrorDescriber.PasswordRequiresDigit) => "پسورد باید حداقل یک عدد داشته باشد.",
                nameof(IdentityErrorDescriber.PasswordMismatch) => "پسورد فعلی اشتباه است.",
                _ => string.IsNullOrWhiteSpace(error.Description) ? "تغییر پسورد انجام نشد." : error.Description
            };
        }
    }
}
