using AutoMapper;
using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Model;
using GeekShopping.CartAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly MySQLContext _context;
        private IMapper _mapper;

        public CartRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<bool> ApplyCoupon(string userId, string couponCode)
        {
            throw new NotImplementedException();
        }
        public Task<bool> RemoveCoupon(string userId)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> ClearCart(string userId)
        {
            //recupera o carrinho
            var cartHeader = await _context.CartHeaders
                        .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cartHeader != null)
            {
                //Remove os CartDetails associados
                _context.CartDetails.RemoveRange(_context.CartDetails
                        .Where(c => c.CartHeaderId == cartHeader.Id));

                //Remove o CartHeader
                _context.Remove(cartHeader);

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CartVO> FindCartByUserId(string userId)
        {
            Cart cart = new()
            {
                CartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId),
            };
            cart.CartDetails = _context.CartDetails.Where(c =>
                    c.CartHeaderId == cart.CartHeader.Id).Include(c => c.Product);

            return _mapper.Map<CartVO>(cart);
        }

        public async Task<bool> RemoveFromCart(long cartDetailsId)
        {
            try
            {
                CartDetail cartDetail = await _context.CartDetails.FirstOrDefaultAsync(c =>
                                            c.Id == cartDetailsId);

                int total = _context.CartDetails.Where(c =>
                            c.CartHeaderId == cartDetail.CartHeaderId).Count();

                //remoçao
                _context.CartDetails.Remove(cartDetail);

                //agora remover o Header
                if (total == 1)
                {
                    //recuperar o CartHeader vinculado ao CartDetail que acabamos de excluir.
                    var cartHeaderToRemove = await _context.CartHeaders
                            .FirstOrDefaultAsync(c => c.Id == cartDetail.CartHeaderId);

                    _context.CartHeaders.Remove(cartHeaderToRemove);
                }

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<CartVO> SaveOrUpdateCart(CartVO cartVo)
        {
            Cart cart = _mapper.Map<Cart>(cartVo);

            var product = await _context.Products.FirstOrDefaultAsync(
                    p => p.Id == cartVo.CartDetails.FirstOrDefault().ProductId);

            //check if the product is already saved in the database if it does not exist then save
            if (product == null)
            {
                _context.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _context.SaveChangesAsync();
            }

            //incluso AsNoTracking para dizer ao EF que não gravaremos as mudanças
            //nesse elemento que buscamos na base.
            var cartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(
                    c => c.UserId == cart.CartHeader.UserId);

            //check if CartHeader is null
            if (cartHeader == null)
            {
                //Create CartHeaders and CartDetails
                _context.CartHeaders.Add(cart.CartHeader);
                await _context.SaveChangesAsync();

                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;

                //Setamos como null porque se formos ver o Model do cartDetail, ja temos o produto e ele
                //já existe nesse contexto, estao precisamos colocar como nulo para que não de conflito.
                cart.CartDetails.FirstOrDefault().Product = null;
                _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
            else
            {
                //If CartHeader is not null
                //Check if CartDetails has same product.
                var cartDetail = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                        p => p.ProductId == cartVo.CartDetails.FirstOrDefault().ProductId &&
                        p.CartHeaderId == cartHeader.Id);

                if (cartDetail == null)
                {
                    //Create CartDetails
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
                else
                {
                    //Update product coint and CartDetails
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetail.Count;
                    cart.CartDetails.FirstOrDefault().Id = cartDetail.Id;
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;

                    //adicionar essas alterações ao contexto
                    _context.CartDetails.Update(cart.CartDetails.FirstOrDefault());

                    await _context.SaveChangesAsync();
                }
            }
            return _mapper.Map<CartVO>(cart);
        }
    }
}
